using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Models
{
    public record EmailSendCommand(
        string MessageId,
        string CorrelationId,
        string IdempotencyKey,
        string ToEmail,
        string Subject,
        string TemplateId,
        Dictionary<string, string> TemplateParams,
        string Source,
        string Purpose,
        DateTimeOffset CreatedAt
        );
}
