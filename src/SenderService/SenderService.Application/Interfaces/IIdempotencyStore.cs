using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Application.Interfaces
{
    public interface IIdempotencyStore
    {
        Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);
        Task MarkAsProcessedAsync(string key, TimeSpan ttl, CancellationToken cancellationToken = default);
    }
}
