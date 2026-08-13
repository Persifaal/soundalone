using AuthService.Application.Interfaces.Manager;
using AuthService.Application.Interfaces.Repositories;
using AuthService.Application.Interfaces.Security;
using AuthService.Application.Interfaces.Services;
using AuthService.Application.Models;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text;

namespace AuthService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IEmailConfirmationCodeStore _emailConfirmationCodeStore;
        private readonly IRedisSessionManager _redisSessionRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(ILogger<AuthService> logger,
            IUserAccountRepository userAccountRepository,
            IRedisSessionManager redisSessionRepository,
            IEmailConfirmationCodeStore emailConfirmationCodeStore,
            ISessionRepository sessionRepository,
            IJwtTokenService jwtTokenService,
            IPasswordHasher passwordHasher)
        {
            _logger = logger;
            _userAccountRepository = userAccountRepository;
            _redisSessionRepository = redisSessionRepository;
            _emailConfirmationCodeStore = emailConfirmationCodeStore;
            _sessionRepository = sessionRepository;
            _jwtTokenService = jwtTokenService;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponseDto> LoginAsync(string email, string password, string? userAgent, string? ipAddress)
        {
            var existingUser = await _userAccountRepository.GetUserByEmail(email, CancellationToken.None);

            if (existingUser == null)
            {
                _logger.LogError("User with email {Email} not found", email);
                throw new InvalidOperationException($"User with email {email} not found");
            }

            if (!_passwordHasher.Verify(password, existingUser.PasswordHash))
            {
                _logger.LogError("Invalid password for user {Email}", email);
                throw new InvalidOperationException("Invalid password");
            }

            return await GenerateAuthResponseAsync(existingUser, userAgent, ipAddress);
        }

        public async Task LogoutAsync(string refreshToken)
        {
            var hash = TokenHash(refreshToken);
            var userSession = await _sessionRepository.GetByRefreshTokenHashAsync(hash, CancellationToken.None);

            await _redisSessionRepository.RevokeSessionAsync(userSession, CancellationToken.None);

            var dbSession = await _sessionRepository.GetByRefreshTokenHashAsync(hash, CancellationToken.None);
            if(dbSession != null)
            {
                dbSession.RevokedAt = DateTime.UtcNow;
                await _sessionRepository.UpdateAsync(dbSession, CancellationToken.None);
                await _sessionRepository.SaveChangesAsync(CancellationToken.None);
            }
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken, string? userAgent, string? ipAddress)
        {
            var hashedRefreshToken = TokenHash(refreshToken);

            var redisSession = await _redisSessionRepository.GetSessionByHashAsync(hashedRefreshToken);
            UserSession dbSession = null;

            if (redisSession != null)
            {
                dbSession = await _sessionRepository.GetSessionByIdAsync(redisSession.Id, CancellationToken.None);
            }
            else 
            {
                dbSession = await _sessionRepository.GetByRefreshTokenHashAsync(refreshToken, CancellationToken.None);
            }

            if (dbSession == null || dbSession.RevokedAt != null || dbSession.ExpiresAt < DateTime.UtcNow)
            {
                _logger.LogError("Invalid or expired refresh token");
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }

            var token = _jwtTokenService.GenerateTokenPair(dbSession.User);
            var newHash = TokenHash(token.RefreshToken);


            dbSession.RevokedAt = DateTime.UtcNow;
            await _sessionRepository.UpdateAsync(dbSession, CancellationToken.None);

            var newSession = new UserSession
            {
                Id = Guid.NewGuid(),
                UserId = dbSession.UserId,
                RefreshTokenHash = newHash,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                UserAgent = userAgent,
                IpAddress = ipAddress
            };

            await _sessionRepository.AddAsync(newSession, CancellationToken.None);
            await _sessionRepository.SaveChangesAsync(CancellationToken.None);

            await _redisSessionRepository.RevokeSessionAsync(newSession, CancellationToken.None);

            var newRedisData = new UserSessionData(newSession.Id, 
                newSession.UserId,
                newHash,
                newSession.CreatedAt,
                newSession.ExpiresAt, 
                userAgent,
                ipAddress);

            await _redisSessionRepository.CreateSessionAsync(newRedisData, TimeSpan.FromDays(7), CancellationToken.None);

            return new AuthResponseDto(
                token.AccessToken,
                token.RefreshToken,
                token.AccessTokenExpiresAtUtc,
                dbSession.UserId,
                dbSession.User.Username,
                dbSession.User.Email,
                dbSession.User.EmailVerified
            );
        }

        public async Task<AuthResponseDto> RegisterAsync(string email, string username, string password, string? phoneNumber, string? userAgent, string? ipAddress)
        {
            var existingUser = await _userAccountRepository.GetUserByEmail(email, CancellationToken.None);

            if (existingUser != null) 
            {
                _logger.LogError("User with email {Email} already exists", email); 
                throw new InvalidOperationException($"User with email {email} already exists");
            }

            var passwordHash = _passwordHasher.Hash(password);

            var userResult = new UserAccount
            {
                Username = username,
                Email = email,
                EmailVerified = false,
                PasswordHash = passwordHash,
                PhoneNumber = phoneNumber,
                CreatedAt = DateTime.UtcNow,
            };

            await _userAccountRepository.CreateUser(userResult, CancellationToken.None);
            await _userAccountRepository.SaveChangesAsync(CancellationToken.None);

            return await GenerateAuthResponseAsync(userResult, userAgent, ipAddress);
        }

        private async Task<AuthResponseDto> GenerateAuthResponseAsync(UserAccount newUser, string? userAgent, string? ipAddress)
        {
            var tokens = _jwtTokenService.GenerateTokenPair(newUser);
            var hash = TokenHash(tokens.RefreshToken);
            var session = new UserSession
            {
                Id = Guid.NewGuid(),
                UserId = newUser.Id,
                RefreshTokenHash = hash,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.Add(TimeSpan.FromDays(7)),
                UserAgent = userAgent,
                IpAddress = ipAddress
            };

            await _sessionRepository.AddAsync(session, CancellationToken.None);
            await _sessionRepository.SaveChangesAsync(CancellationToken.None);

            var redisData = new UserSessionData(session.Id, session.UserId, hash, session.CreatedAt, session.ExpiresAt, userAgent, ipAddress);
            await _redisSessionRepository.CreateSessionAsync(redisData, TimeSpan.FromDays(7), CancellationToken.None);

            return new AuthResponseDto(
            AccessToken: tokens.AccessToken,
            RefreshToken: tokens.RefreshToken,
            AccessTokenExpiresAtUtc: tokens.AccessTokenExpiresAtUtc,
            UserId: newUser.Id,
            Username: newUser.Username,
            Email: newUser.Email,
            IsEmailVerified: newUser.EmailVerified
        );
        }

        private static string TokenHash(string token)
        {
            var bytes = Encoding.UTF8.GetBytes(token);
            var hashBytes = System.Security.Cryptography.SHA256.HashData(bytes);
            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }

        public async Task ConfirmEmailAsync(string email, string code, CancellationToken cancellationToken = default)
        {
            var user = await _userAccountRepository.GetUserByEmail(email, cancellationToken);

            if (user.EmailVerified == false) 
            {
                return;
            }

            var storedCode = await _emailConfirmationCodeStore.GetAsync(user.Id, cancellationToken);
            if (storedCode == null)
            {
                throw new InvalidOperationException("Код подтверждения не найден или уже был использован");
            }

            if (storedCode.ExpiresAtUtc < DateTime.UtcNow)
            {
                await _emailConfirmationCodeStore.DeleteAsync(user.Id, cancellationToken);
                throw new InvalidOperationException("Срок действия кода истек. Запросите новый.");
            }

            if (storedCode.Attempts >= storedCode.MaxAttempts)
            {
                await _emailConfirmationCodeStore.DeleteAsync(user.Id, cancellationToken);
                throw new InvalidOperationException("Превышено максимальное количество попыток. Запросите новый код.");
            }

            var verificationResult = _passwordHasher.Verify(code, storedCode.HashCode);

            if (!verificationResult)
            {
                storedCode.Attempts++;
                await _emailConfirmationCodeStore.SaveAsync(user.Id, storedCode, cancellationToken);

                throw new InvalidOperationException("Неверный код подтверждения");
            }

            await _userAccountRepository.SetVerificationStatusAsync(user.Id, cancellationToken);

            await _emailConfirmationCodeStore.DeleteAsync(user.Id, cancellationToken);
        }
    }
}
