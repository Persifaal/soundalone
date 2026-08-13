using SenderService.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Application.Interfaces
{
    public interface IEmailProcessor
    {
        Task ProcessAsync(EmailSendCommand command, CancellationToken cancellationToken);
    }
}
