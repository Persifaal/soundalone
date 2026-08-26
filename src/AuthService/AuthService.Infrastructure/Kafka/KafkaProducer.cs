using AuthService.Application.Interfaces.Services;
using AuthService.Application.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using SoundAlone.Shared.Contracts.Events;

namespace AuthService.Infrastructure.Kafka;

public class KafkaProducer : IKafkaEventPublisher
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;

    private const string EmailSendTopic = "email-send-commands";
    private const string UserRegisteredTopic = "user-registered";

    public KafkaProducer(
        IProducer<string, string> producer,
        ILogger<KafkaProducer> logger)
    {
        _producer = producer;
        _logger = logger;
    }

    public async Task PublishAsync(
        EmailSendCommand command,
        CancellationToken ct = default)
    {
        var json = JsonSerializer.Serialize(command);

        var message = new Message<string, string>
        {
            Key = command.IdempotencyKey,
            Value = json,
            Headers = new Headers
            {
                {
                    "message_id",
                    Encoding.UTF8.GetBytes(command.MessageId)
                },
                {
                    "correlation_id",
                    Encoding.UTF8.GetBytes(command.CorrelationId)
                },
                {
                    "idempotency_key",
                    Encoding.UTF8.GetBytes(command.IdempotencyKey)
                },
                {
                    "type",
                    Encoding.UTF8.GetBytes("email.send.command")
                },
                {
                    "source",
                    Encoding.UTF8.GetBytes(command.Source)
                },
                {
                    "purpose",
                    Encoding.UTF8.GetBytes(command.Purpose)
                }
            }
        };

        await PublishAsync(
            EmailSendTopic,
            message,
            ct);
    }

    public async Task PublishAsync(
        UserRegisteredEvent @event,
        CancellationToken ct = default)
    {
        _logger.LogInformation(
            "Publishing UserRegisteredEvent. UserId: {UserId}",
            @event.UserId);

        var json = JsonSerializer.Serialize(@event);

        _logger.LogInformation(
            "UserRegisteredEvent JSON: {Json}",
            json);

        var message = new Message<string, string>
        {
            Key = @event.UserId.ToString(),
            Value = json,
            Headers = new Headers
            {
                {
                    "message_id",
                    Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())
                },
                {
                    "type",
                    Encoding.UTF8.GetBytes("user.registered")
                },
                {
                    "source",
                    Encoding.UTF8.GetBytes("auth-service")
                }
            }
        };

        await PublishAsync(
            UserRegisteredTopic,
            message,
            ct);
    }

    private async Task PublishAsync(
        string topic,
        Message<string, string> message,
        CancellationToken ct)
    {
        try
        {
            var result = await _producer.ProduceAsync(
                topic,
                message,
                ct);

            _logger.LogInformation(
                "Kafka message published. Topic: {Topic}, Partition: {Partition}, Offset: {Offset}",
                result.Topic,
                result.Partition,
                result.Offset);
        }
        catch (KafkaException ex)
        {
            _logger.LogError(
                ex,
                "Failed to publish Kafka message to topic {Topic}",
                topic);

            throw;
        }
    }
}