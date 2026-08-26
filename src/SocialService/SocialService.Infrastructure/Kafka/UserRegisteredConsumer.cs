using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SocialService.Application.Interfaces;
using SoundAlone.Shared.Contracts.Events;

namespace SocialService.Infrastructure.Kafka;

public class UserRegisteredConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConsumer<string, string> _consumer;
    private readonly ILogger<UserRegisteredConsumer> _logger;

    private const string Topic = "user-registered";

    public UserRegisteredConsumer(
        IServiceScopeFactory scopeFactory,
        IConsumer<string, string> consumer,
        ILogger<UserRegisteredConsumer> logger)
    {
        _scopeFactory = scopeFactory;
        _consumer = consumer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        _consumer.Subscribe(Topic);

        _logger.LogInformation(
            "Subscribed to Kafka topic {Topic}",
            Topic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = _consumer.Consume(stoppingToken);

                    if (result?.Message?.Value == null)
                        continue;

                    var @event =
                        JsonSerializer.Deserialize<UserRegisteredEvent>(
                            result.Message.Value);

                    if (@event == null)
                    {
                        _logger.LogWarning(
                            "Failed to deserialize UserRegisteredEvent");

                        continue;
                    }

                    using var scope =
                        _scopeFactory.CreateScope();

                    var profileService =
                        scope.ServiceProvider
                            .GetRequiredService<IUserProfileService>();

                    await profileService
                        .CreateFromUserRegisteredAsync(
                            @event,
                            stoppingToken);

                    _consumer.Commit(result);

                    _logger.LogInformation(
                        "Processed UserRegistered event for user {UserId}",
                        @event.UserId);
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(
                        ex,
                        "Kafka consume error");
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error processing UserRegistered event");
                }
            }
        }
        catch (OperationCanceledException)
        {
            // graceful shutdown
        }
        finally
        {
            _consumer.Close();
        }
    }
}