using AuthService.Application.Models;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Security
{
    public class JwtTokenService
    {
        private readonly AuthOptions _options;
        private readonly byte[] _secretKey;

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
                issuer: null,
                audience: null,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenMinutes),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials
                (new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha256));

            var refreshToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)).ToLowerInvariant();

            return new TokenPair(
                _handler.WriteToken(token),
                DateTime.UtcNow.AddMinutes(_options.AccessTokenMinutes),
                jti,
                refreshToken,
                DateTime.UtcNow.AddDays(_options.RefreshTokenDays));
        }

        public bool ValidateAccessToken(string token, out string? jti)
        {
            jti = null;
            // Implement token validation logic here
            return true;
        }
}
