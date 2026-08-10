using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Application.Interfaces
{
    public interface IEmailProvider
    {
        public Task SendAsync(
           string toEmail,
           string subject,
           string htmlBody,
           string? textBody = null,
           CancellationToken cancellationToken = default);
    }
}
