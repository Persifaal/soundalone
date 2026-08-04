using AuthService.Application.Interfaces.Repositories;
using AuthService.Application.Models;
using AuthService.Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.RedisSession
{
    public class RedisSessionRepository : IRedisSessionRepository
    {
        private readonly IDatabase _redis;
        private const string KeyPrefix = "auth:rt:";

        public RedisSessionRepository(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        public async Task CreateSessionAsync(UserSessionData sessionData, TimeSpan ttl, CancellationToken ct = default)
        {
            var key = $"{KeyPrefix}{sessionData.RefreshTokenHash}";
            var json = JsonSerializer.Serialize(sessionData);
            await _redis.StringSetAsync(key, json, ttl);
        }

        public async Task<UserSession?> GetSessionByHashAsync(string refreshTokenHash, CancellationToken ct = default)
        {
            var key = $"{KeyPrefix}{refreshTokenHash}";
            var json = await _redis.StringGetAsync(key);
            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<UserSession>(json);
        }

        public async Task RevokeSessionAsync(UserSession session, CancellationToken ct = default)
        {
            var key = $"{KeyPrefix}{session.RefreshTokenHash}";
            await _redis.KeyDeleteAsync(key);
        }
    }
}
