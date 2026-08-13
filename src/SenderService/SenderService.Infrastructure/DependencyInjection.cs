using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenderService.Application.Interfaces;
using SenderService.Application.Services;
using SenderService.Infrastructure.Email;
using SenderService.Infrastructure.Kafka;
using SenderService.Infrastructure.Redis;
using StackExchange.Redis;

namespace SenderService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration.GetConnectionString("Redis")
            ?? throw new InvalidOperationException("Redis connection string is missing.");

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            ConnectionMultiplexer.Connect(redisConnectionString));

            var kafkaConnectionString = configuration.GetConnectionString("Kafka")
               ?? throw new InvalidOperationException("Kafka connection string is missing.");

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = kafkaConnectionString,
                GroupId = configuration["Kafka:GroupId"] ?? "sender-service",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
                EnablePartitionEof = true
            };

            services.AddSingleton(consumerConfig);

            services.AddSingleton<IEmailProvider, SmtpEmailProvider>();
            services.AddSingleton<IIdempotencyStore, IdempotencyStore>();
            services.AddSingleton<IEmailProcessor, EmailProcessor>();

            services.AddHostedService<EmailKafkaConsumer>();

            return services;
        }
    }
}
