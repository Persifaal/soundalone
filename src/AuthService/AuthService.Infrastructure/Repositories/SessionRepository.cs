using AuthService.Application.Interfaces.Repositories;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AuthDbContext _dbContext;

        public SessionRepository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(UserSession session, CancellationToken cancellationToken)
        {
            await _dbContext.UserSessions.AddAsync(session, cancellationToken);
        }

        public async Task<UserSession?> GetByRefreshTokenHashAsync(string refreshTokenHash, CancellationToken cancellationToken)
        {
            return await _dbContext.UserSessions.FirstOrDefaultAsync(
                s => s.RefreshTokenHash == refreshTokenHash &&
                s.ExpiresAt > DateTime.UtcNow, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        } 
    }
}
