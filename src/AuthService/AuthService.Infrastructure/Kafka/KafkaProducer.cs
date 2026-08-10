using AuthService.Application.Interfaces.Services;
using AuthService.Application.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace AuthService.Infrastructure.Kafka
{
    public class KafkaProducer : IKafkaEventPublisher
    {
        private readonly IProducer<string, string> _producer;
        private readonly ILogger<KafkaProducer> _logger;
        private const string Topic = "email-send-commands";

        public KafkaProducer(IProducer<string, string> producer, ILogger<KafkaProducer> logger)
        {
            _producer = producer;
            _logger = logger;
        }

        public async Task PublishAsync(EmailSendCommand command, CancellationToken ct = default)
        {
            var json = JsonSerializer.Serialize(command);

            var message = new Message<string, string>
            {
                Key = command.IdempotencyKey,
                Value = json
            };

            try
            {
                message.Headers ??= new Headers();

                message.Headers.Add("message_id", Encoding.UTF8.GetBytes(command.MessageId));
                message.Headers.Add("correlation_id", Encoding.UTF8.GetBytes(command.CorrelationId));
                message.Headers.Add("idempotency_key", Encoding.UTF8.GetBytes(command.IdempotencyKey));
                message.Headers.Add("type", Encoding.UTF8.GetBytes("email.send.command"));
                message.Headers.Add("source", Encoding.UTF8.GetBytes(command.Source));
                message.Headers.Add("purpose", Encoding.UTF8.GetBytes(command.Purpose));

                var result = await _producer.ProduceAsync(Topic, message, ct);

                _logger.LogInformation("Message published to Kafka topic {Topic} with offset {Offset}", result.TopicPartitionOffset.Topic, result.TopicPartitionOffset.Offset);
            }
            catch(KafkaException ex)
            {
                _logger.LogError(ex, "Error occurred while publishing message to Kafka");
                throw;
            }
        }
    }
}
