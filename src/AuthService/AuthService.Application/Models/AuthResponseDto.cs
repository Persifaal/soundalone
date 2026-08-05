using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Models
{
    public record AuthResponseDto(
        string AccessToken,
        string RefreshToken,
        DateTime AccessTokenExpiresAtUtc,
        Guid UserId,
        string Username,
        string Email,
        bool IsEmailVerified
    );
}
