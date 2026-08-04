using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Models
{
    public record TokenPair(
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string AccessTokenJti,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc);
}
