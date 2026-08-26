using AuthService.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<AuthResponseDto> RegisterAsync(string email, string username, string password, string? phoneNumber, string? userAgent, string? ipAddress);
        public Task<AuthResponseDto> LoginAsync(string email, string password, string? userAgent, string? ipAddress);
        public Task<AuthResponseDto> RefreshTokenAsync(string refreshToken, string? userAgent, string? ipAddress);
        public Task ConfirmEmailAsync(string email, string code, CancellationToken cancellationToken = default);
        public Task LogoutAsync(string refreshToken);
        public Task<AccessTokenPayload> MeAsync(string refreshToken);
        
    }
}
