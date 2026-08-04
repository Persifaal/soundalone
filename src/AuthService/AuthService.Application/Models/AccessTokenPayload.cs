using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Models
{
    public record AccessTokenPayload(
        Guid Userid,
        string Email,
        string Jti,
        DateTime ExpiresAtUtc);
}
