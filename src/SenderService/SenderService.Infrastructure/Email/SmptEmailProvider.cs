using Microsoft.Extensions.Options;
using SenderService.Application.Interfaces;
using SenderService.Application.Model;

namespace SenderService.Infrastructure.Email
{
    public class SmtpEmailProvider : IEmailProvider
    {
        private readonly SmtpOptions _options;

        public SmtpEmailProvider(IOptions<SmtpOptions> options)
        {
            _options = options.Value;
        }

        public Task SendAsync(string toEmail, string subject, string htmlBody, string? textBody = null, CancellationToken cancellationToken = default)
        {
            
        }
    }
}
