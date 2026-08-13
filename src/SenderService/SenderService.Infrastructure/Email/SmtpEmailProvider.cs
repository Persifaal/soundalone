using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SenderService.Application.Interfaces;
using SenderService.Application.Model;
using System.Data;

namespace SenderService.Infrastructure.Email
{
    public class SmtpEmailProvider : IEmailProvider
    {
        private readonly SmtpOptions _options;

        public SmtpEmailProvider(IOptions<SmtpOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendAsync(
            string toEmail,
            string subject, 
            string htmlBody,
            string? textBody = null,
            CancellationToken cancellationToken = default)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_options.FromName, _options.FromEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            var body = new BodyBuilder 
            {
                HtmlBody = htmlBody,
                TextBody = textBody,
            };

            message.Body = body.ToMessageBody();

            var client = new SmtpClient();

            await client.ConnectAsync(_options.Host, _options.Port, SecureSocketOptions.None, cancellationToken);
            await client.AuthenticateAsync(_options.Username, _options.Password, cancellationToken);
            await client.SendAsync(message, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
