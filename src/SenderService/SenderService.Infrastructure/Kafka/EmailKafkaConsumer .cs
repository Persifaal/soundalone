using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SenderService.Application.Interfaces;
using SenderService.Application.Model;
using System.Text;
using System.Text.Json;

namespace SenderService.Infrastructure.Kafka
{
    public class EmailKafkaConsumer : BackgroundService
    {
        private readonly ILogger<EmailKafkaConsumer> _logger;
        private readonly IConsumer<string, string> _consumer;
        private readonly IEmailProcessor _emailProcessor;
        private readonly string[] _topics;

        public EmailKafkaConsumer(
            IConfiguration configuration,
            ILogger<EmailKafkaConsumer> logger,
            IEmailProcessor emailProcessor,
            ConsumerConfig config)
        {
            _topics = configuration.GetSection("Kafka:SubscribedTopics")
               .GetChildren()
               .Select(c => c.Value)
               .ToArray();

            _logger = logger;
            _emailProcessor = emailProcessor;

            _consumer = new ConsumerBuilder<string, string>(config)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Utf8)
                .SetErrorHandler((_, e) => _logger.LogError("Kafka Error: {Reason}", e.Reason))
                .Build();
        }

        protected override  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(_topics);
            _logger.LogInformation("Subscribed to Kafka topics: {Topics}", string.Join(", ", _topics));

            // Запускаем в отдельной задаче, чтобы не блокировать старт хоста
            return Task.Run(() => ProcessMessages(stoppingToken), stoppingToken);
        }

        private async Task ProcessMessages(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);

                    // ✅ ИСПРАВЛЕНИЕ 1: Проверяем, что именно Message не null, а не весь consumeResult
                    if (consumeResult?.Message is null)
                    {
                        continue;
                    }

                    var messageId = GetHeader(consumeResult.Message, "message_id");
                    var idempotencyKey = GetHeader(consumeResult.Message, "idempotency_key");

                    _logger.LogInformation("Received message. MessageId: {MessageId}, IdempotencyKey: {IdempotencyKey}", messageId, idempotencyKey);

                    var command = JsonSerializer.Deserialize<EmailSendCommand>(
                        consumeResult.Message.Value,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    if (command is null)
                    {
                        _logger.LogError("Failed to deserialize message. Skipping and committing.");
                        _consumer.Commit(consumeResult);
                        continue;
                    }

                    try
                    {
                        _logger.LogInformation("Processing email for: {Email}...", command.ToEmail);

                        await _emailProcessor.ProcessAsync(command, stoppingToken);

                        // Коммитим только при успехе
                        _consumer.Commit(consumeResult);
                        _logger.LogInformation("✅ Email sent successfully. MessageId: {MessageId}", messageId);
                    }
                    catch (Exception ex)
                    {
                        // ✅ ИСПРАВЛЕНИЕ 2: Передаем сам объект 'ex' первым аргументом, чтобы увидеть полный Stack Trace в логах
                        _logger.LogError(ex, "💥 Unexpected Error processing message. MessageId: {MessageId}. Error: {ErrorMessage}", messageId, ex.Message);

                        // ✅ ИСПРАВЛЕНИЕ 3: ОБЯЗАТЕЛЬНО коммитим offset даже при ошибке!
                        // Иначе Kafka будет бесконечно отдавать нам это же сообщение, вызывая бесконечный цикл ошибок.
                        // (В реальном продакшене здесь сообщение отправляют в Dead Letter Queue).
                        _consumer.Commit(consumeResult);
                    }
                }
                catch (ConsumeException e)
                {
                    _logger.LogError(e, "Kafka Consume Error: {Error}", e.Error.Reason);
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Consumer stopped gracefully.");
                    break;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Unexpected fatal error in Kafka loop: {Message}", e.Message);
                }
            }
        }

        private string? GetHeader(Message<string, string> message, string key)
        {
            if (message.Headers?.TryGetLastBytes(key, out var bytes) == true)
            {
                return Encoding.UTF8.GetString(bytes);
            }
            return null;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping Kafka consumer...");
            _consumer.Close();
            return base.StopAsync(cancellationToken);
        }
    }
}