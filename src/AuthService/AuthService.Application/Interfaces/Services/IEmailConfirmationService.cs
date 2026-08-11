using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces.Services
{
    public interface IEmailConfirmationService
    {
        public Task SendConfirmationCodeASync(Guid userId, string email, CancellationToken cancellationToken = default);
    }
}
