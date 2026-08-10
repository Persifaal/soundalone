using AuthService.Application.Interfaces.Manager;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Application.Interfaces.Security;
using AuthService.Application.Interfaces.Services;
using AuthService.Application.Models;
using AuthService.Infrastructure.Kafka;
using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Redis;
using AuthService.Infrastructure.RedisSession;
using AuthService.Infrastructure.Repositories;
using AuthService.Infrastructure.Security;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace AuthService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions => 
                {
                    npgsqlOptions.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName);
                    npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                }
            ));

        var redisConnectionString = configuration.GetConnectionString("Redis")
            ?? throw new InvalidOperationException("Redis connection string is missing.");

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = configuration.GetConnectionString("Kafka")
                ?? throw new InvalidOperationException("Kafka connection string is missing."),
            Acks = Acks.All,
            EnableIdempotence = true,
            LingerMs = 5,
        };

        services.AddSingleton<IConnectionMultiplexer>(sp =>
            ConnectionMultiplexer.Connect(redisConnectionString));

        services.Configure<AuthOptions>(configuration.GetSection("Auth"));

        services.AddScoped<IUserAccountRepository, UserAccountRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IEmailConfirmationCodeStore, RedisEmailConfirmationCodeStore>();
        services.AddScoped<IRedisSessionManager, RedisSessionManager>();
        services.AddSingleton<IKafkaEventPublisher, KafkaProducer>();
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        
        services.AddSingleton<IProducer<string, string>>(sp =>
        {
            return new ProducerBuilder<string, string>(producerConfig).Build();
        });

        return services;
    }
}
