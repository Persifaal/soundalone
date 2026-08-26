using AuthService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundAlone.Shared.Contracts.Events;

namespace AuthService.Application.Interfaces.Services
{
    public interface IKafkaEventPublisher
    {
        public Task PublishAsync(EmailSendCommand command, CancellationToken ct = default);
        public Task PublishAsync(UserRegisteredEvent @event, CancellationToken ct = default);
    }
}
