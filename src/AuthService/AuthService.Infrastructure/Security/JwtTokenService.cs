using AuthService.Application.Interfaces.Services;
using AuthService.Application.Models;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Infrastructure.Security
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly AuthOptions _options;
        private readonly byte[] _secretKey;
        private readonly JwtSecurityTokenHandler _handler = new();

        public JwtTokenService(IOptions<AuthOptions> options)
        {
            _options = options.Value;
            _secretKey = Encoding.UTF8.GetBytes(_options.JwtSecret);
        }

        public TokenPair GenerateTokenPair(UserAccount user)
        {
            var jti = Guid.NewGuid().ToString("N");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, jti)
            };

            var token = new JwtSecurityToken(
                issuer: _options.JwtIssuer,
                audience: _options.JwtAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenMinutes),
                signingCredentials: new SigningCredentials
                (new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha256));

            var refreshToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)).ToLowerInvariant();

            return new TokenPair(
                _handler.WriteToken(token),
                DateTime.UtcNow.AddMinutes(_options.AccessTokenMinutes),
                jti,
                refreshToken,
                DateTime.UtcNow.AddDays(_options.RefreshTokenDays));
        }

        public bool ValidateAccessToken(string token, out AccessTokenPayload accessToken)
        {
            accessToken = default;

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _options.JwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = _options.JwtAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secretKey),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = _handler.ValidateToken(token, validationParameters, out var securityToken);
                if (securityToken is not JwtSecurityToken jwtToken)
                {
                    return false;
                }

                var sub = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                var email = principal.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
                var jti = principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
                var exp = jwtToken.ValidTo;

                if (!Guid.TryParse(sub, out var userId) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(jti))
                {
                    return false;
                }

                accessToken = new AccessTokenPayload(userId, email, jti, exp);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
