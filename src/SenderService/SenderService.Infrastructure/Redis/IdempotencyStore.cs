using SenderService.Application.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Infrastructure.Redis
{
    public class IdempotencyStore : IIdempotencyStore
    {
        private readonly IDatabase _redis;

        public IdempotencyStore(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
        {
            return await _redis.KeyExistsAsync(GetKey(key));    
        }

        public async Task MarkAsProcessedAsync(string key, TimeSpan ttl, CancellationToken cancellationToken = default)
        {
            await _redis.StringSetAsync(GetKey(key), "processed", ttl);
        }

        private string GetKey(string key) => $"idempotency:email_sender:{key}";
    }
}
