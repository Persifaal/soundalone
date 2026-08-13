using AuthService.Application.Generator;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Application.Interfaces.Security;
using AuthService.Application.Interfaces.Services;
using AuthService.Application.Models;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Services
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly ILogger<EmailConfirmationService> _logger;
        private readonly IEmailConfirmationCodeStore _emailConfirmationCode;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IKafkaEventPublisher _kafkaEventPublisher;

        public EmailConfirmationService(
            ILogger<EmailConfirmationService> logger,
            IEmailConfirmationCodeStore emailConfirmationCode,
            IUserAccountRepository userAccountRepository,
            IPasswordHasher passwordHasher,
            IKafkaEventPublisher kafkaEventPublisher) 
        {
            _logger = logger;
            _emailConfirmationCode = emailConfirmationCode;
            _userAccountRepository = userAccountRepository;
            _passwordHasher = passwordHasher;
            _kafkaEventPublisher = kafkaEventPublisher;
        }

        public async Task SendConfirmationCodeASync(Guid userId, string email, CancellationToken cancellationToken = default ) 
        {
            var code = OneTimeCodeGenerator.Generate();

            var codeHash = _passwordHasher.Hash(code);
            
            var utcNow = DateTime.UtcNow;

            var confirmation = new ConfirmnationData 
            {
                CodeId = Guid.NewGuid(),
                HashCode = codeHash,
                Attempts = 0,
                MaxAttempts = 3,
                CreatedAtUtc = utcNow,
                ExpiresAtUtc = utcNow.AddMinutes(2),
            };
            
            await _emailConfirmationCode.SaveAsync(userId, confirmation, cancellationToken);

            var command = new EmailSendCommand(
                MessageId: Guid.NewGuid().ToString(),
                CorrelationId: Guid.NewGuid().ToString(),
                IdempotencyKey: confirmation.CodeId.ToString(),
                ToEmail: email,
                Subject: "Email Confirmation Code",
                TemplateId: "auth.email_confirmation",
                TemplateParams: new Dictionary<string, string>
                {
                    ["code"] = code,
                    ["email"] = email
                },
                Source: "AuthService",
                Purpose: "email_confirmation",
                CreatedAt: utcNow
                );

            await _kafkaEventPublisher.PublishAsync(command, cancellationToken);
        }
    }
}
