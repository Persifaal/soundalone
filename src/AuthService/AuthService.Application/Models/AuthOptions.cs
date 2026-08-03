using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Models
{
    public class AuthOptions
    {
        public string JwtIssuer { get; init; } = "SoundAlone.AuthService";
        public string JwtAudience { get; init; } = "SoundAlone.Services";
        public string JwtSecret { get; init; } = string.Empty;
        public int AccessTokenMinutes { get; init; } = 15;
        public int RefreshTokenDays { get; init; } = 7;
    }
}
