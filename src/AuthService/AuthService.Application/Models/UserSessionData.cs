using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Models
{
    public record UserSessionData(
        Guid SessionId,
        Guid UserId,
        string RefreshTokenHash,
        DateTime CreatedAtUtc,
        DateTime ExpiresAtUtc,
        string? UserAgent,
        string? IpAddress
        );
}
