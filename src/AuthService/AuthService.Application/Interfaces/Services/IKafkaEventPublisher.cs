using AuthService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces.Services
{
    public interface IKafkaEventPublisher
    {
        public Task PublishAsync(EmailSendCommand command, CancellationToken ct = default);
    }
}
