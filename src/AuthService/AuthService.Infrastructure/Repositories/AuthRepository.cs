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
    public sealed class AuthRepository
    {
        private readonly AuthDbContext _context;

        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task CreateUser(UserAccount user, CancellationToken cancellationToken) 
        {
            await _context.UserAccounts.AddAsync(user, cancellationToken);
        }

        public async Task UpdateUser(UserAccount user)
        {
            _context.UserAccounts.Update(user);
        }

        public async Task<bool> DeleteUser(UserAccount user) 
        { 
            _context.UserAccounts.Remove(user);
            return true;
        }

        public async Task<UserAccount?> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<UserAccount?> GetUserById(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
