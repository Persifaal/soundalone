using Grpc.Core;
using SoundAlone.Contracts.Auth.V1;
using AuthService.Application.Interfaces.Services;

namespace AuthService.Api.Grpc;

public class AuthGrpcService : SoundAlone.Contracts.Auth.V1.AuthService.AuthServiceBase
{
    private readonly IAuthService _authService;

    public AuthGrpcService(IAuthService authService)
    {
        _authService = authService;
    }

    public override async Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
    {
        try
        {
            var tokens = await _authService.RegisterAsync(
                request.Email,
                request.Username,
                request.Password,
                request.PhoneNumber,
                request.UserAgent,
                request.IpAddress);

            return new RegisterResponse
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                AccessTokenExpiresAtUnix = ((DateTimeOffset)tokens.AccessTokenExpiresAtUtc).ToUnixTimeSeconds(),
                UserId = tokens.UserId.ToString(),
                Username = tokens.Username,
                Email = tokens.Email,
                IsEmailVerified = tokens.IsEmailVerified
            };
        }
        catch (ArgumentException ex)
        {
            throw new RpcException(new Status(StatusCode.AlreadyExists, ex.Message));
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
    {
        try
        {
            var tokens = await _authService.LoginAsync(
                request.Email, 
                request.Password, 
                request.UserAgent, 
                request.IpAddress);

            return new LoginResponse
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                AccessTokenExpiresAtUnix = ((DateTimeOffset)tokens.AccessTokenExpiresAtUtc).ToUnixTimeSeconds(),
                UserId = tokens.UserId.ToString(),
                Username = tokens.Username,
                Email = tokens.Email,
                IsEmailVerified = tokens.IsEmailVerified
            };
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, ex.Message));
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<LoginResponse> Refresh(RefreshRequest request, ServerCallContext context)
    {
        try
        {
            var tokens = await _authService.RefreshTokenAsync(
                request.RefreshToken, 
                request.UserAgent, 
                request.IpAddress);

            return new LoginResponse
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                AccessTokenExpiresAtUnix = ((DateTimeOffset)tokens.AccessTokenExpiresAtUtc).ToUnixTimeSeconds(),
                UserId = tokens.UserId.ToString(),
                Username = tokens.Username,
                Email = tokens.Email,
                IsEmailVerified = tokens.IsEmailVerified
            };
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, ex.Message));
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<LogoutResponse> Logout(LogoutRequest request, ServerCallContext context)
    {
        try
        {
            await _authService.LogoutAsync(request.RefreshToken);
            return new LogoutResponse { Success = true };
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }
}