using AuthService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces.Repositories
{
    public interface IEmailConfirmationCodeStore
    {
        public Task SaveAsync(Guid userId, ConfirmnationData data, CancellationToken cancellationToken = default);
        public Task<ConfirmnationData?> GetAsync(Guid userId, CancellationToken cancellationToken = default);
        public Task DeleteAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
