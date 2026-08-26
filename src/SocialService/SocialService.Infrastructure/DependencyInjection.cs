using Confluent.Kafka; // <-- Add this using statement
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialService.Application.Interfaces;
using SocialService.Infrastructure.Kafka;
using SocialService.Infrastructure.Persistence;
using SocialService.Infrastructure.Repositories;

namespace SocialService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SocialDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsAssembly(
                        typeof(SocialDbContext).Assembly.FullName);

                    npgsqlOptions.MigrationsHistoryTable(
                        "__EFMigrationsHistory",
                        "public");
                }));
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

        services.AddSingleton<IConsumer<string, string>>(_ =>
            new ConsumerBuilder<string, string>(consumerConfig)
                .Build());

        services.AddHostedService<UserRegisteredConsumer>();

        services.AddScoped<IUserProfileRepository, UserProfileRepository>();

        return services;
    }
}