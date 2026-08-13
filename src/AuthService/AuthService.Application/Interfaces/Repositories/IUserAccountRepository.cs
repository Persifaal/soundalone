using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces.Repositories
{
    public interface IUserAccountRepository
    {
        public Task CreateUser(UserAccount user, CancellationToken cancellationToken);
        public Task UpdateUser(UserAccount user);
        public Task<bool> DeleteUser(UserAccount user);
        public Task<UserAccount?> GetUserByEmail(string email, CancellationToken cancellationToken);
        public Task<UserAccount?> GetUserById(Guid userId, CancellationToken cancellationToken);
        public Task<bool> SetVerificationStatusAsync(Guid userId, CancellationToken cancellationToken);
        public Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
