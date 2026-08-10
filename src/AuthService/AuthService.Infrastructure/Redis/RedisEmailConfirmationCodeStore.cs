using AuthService.Application.Interfaces.Repositories;
using AuthService.Application.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Redis
{
    public class RedisEmailConfirmationCodeStore : IEmailConfirmationCodeStore
    {
        private readonly IDatabase _redis;
        private const string Purpose = "email_confirmation";

        public RedisEmailConfirmationCodeStore(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        public async Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var key = GetKey(userId);
            await _redis.KeyDeleteAsync(key);
        }

        public async Task<ConfirmnationData?> GetAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var key = GetKey(userId);
            var json = await _redis.StringGetAsync(key);

            return JsonSerializer.Deserialize<ConfirmnationData>(json);
        }

        public async Task SaveAsync(Guid userId, ConfirmnationData data, CancellationToken cancellationToken = default)
        {
            var key = GetKey(userId);
            var json = JsonSerializer.Serialize(data);
            var ttl = data.ExpiresAtUtc - DateTime.UtcNow;

            await _redis.StringSetAsync(key, json, ttl);
        }

        private string GetKey(Guid userId) 
        {
            return $"otp:{Purpose}:{userId}";
        }
    }
}
