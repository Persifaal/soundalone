using AuthService.Application.Models;
using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces.Repositories
{
    public interface IRedisSessionRepository
    {
        public Task CreateSessionAsync(UserSession session, TimeSpan ttl, CancellationToken ct = default);

        public Task<UserSession?> GetSessionByHashAsync(string refreshTokenHash, CancellationToken ct = default);

        public Task RevokeSessionAsync(UserSession session, CancellationToken ct = default);
    }
}
