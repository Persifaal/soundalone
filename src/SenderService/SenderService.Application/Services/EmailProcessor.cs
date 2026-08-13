using Microsoft.Extensions.Logging;
using SenderService.Application.Interfaces;
using SenderService.Application.Model;

namespace SenderService.Application.Services
{
    public class EmailProcessor : IEmailProcessor
    {
        private readonly ILogger<EmailProcessor> _logger;
        private readonly IEmailProvider _emailProvider;
        private readonly IIdempotencyStore _idempotencyStore;
        private readonly IEmailTemplateRenderer _templateRenderer;

        public EmailProcessor(
            ILogger<EmailProcessor> logger,
            IEmailProvider emailProvider,
            IIdempotencyStore idempotencyStore,
            IEmailTemplateRenderer templateRenderer)
        {
            _templateRenderer = templateRenderer;
            _logger = logger;
            _emailProvider = emailProvider;
            _idempotencyStore = idempotencyStore;
        }

        public async Task ProcessAsync(EmailSendCommand command, CancellationToken cancellationToken)
        {
            var exists = await _idempotencyStore.ExistsAsync(command.IdempotencyKey, cancellationToken);
            if (exists) 
            {
                _logger.LogInformation("Email with idempotency key {IdempotencyKey} has already been processed. Skipping.",
                    command.IdempotencyKey, 
                    command.MessageId);
                return;
            }

            var htmlBody = _templateRenderer.Render(command.TemplateId, command.TemplateParams);

            var textBody = command.TemplateParams.TryGetValue("code", out var code)
                ? $"Ваш код подтверждения: {code}"
                : htmlBody;

            await _emailProvider.SendAsync(
                command.ToEmail,
                command.Subject,
                htmlBody,
                textBody,
                cancellationToken);

            await _idempotencyStore.MarkAsProcessedAsync(
                command.IdempotencyKey, 
                TimeSpan.FromDays(1),
                cancellationToken);

            _logger.LogInformation(
                "Email sent successfully. To: {Email}, Purpose: {Purpose}, MessageId: {MessageId}",
                command.ToEmail,
                command.Purpose,
                command.MessageId
            );

        }
    }
}
