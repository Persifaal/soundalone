using Grpc.Core;
using SoundAlone.Contracts.Auth.V1;
using AuthService.Application.Interfaces.Services;

namespace AuthService.Api.Grpc;

public class AuthGrpcService : SoundAlone.Contracts.Auth.V1.AuthService.AuthServiceBase
{
    private readonly IAuthService _authService;
    private readonly IEmailConfirmationService _emailConfirmationService;

    public AuthGrpcService(IAuthService authService, IEmailConfirmationService emailConfirmationService)
    {
        _authService = authService;
        _emailConfirmationService = emailConfirmationService;
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

            await _emailConfirmationService.SendConfirmationCodeASync(tokens.UserId, tokens.Email, context.CancellationToken);

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

            if (!tokens.IsEmailVerified) 
            {
                await _emailConfirmationService.SendConfirmationCodeASync(tokens.UserId, tokens.Email, context.CancellationToken);
            }

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

    public override async Task<EmailResponse> Resend(EmailRequest request, ServerCallContext context)
    {
        try 
        {
            var userId = Guid.Parse(request.UserId);

            await _emailConfirmationService.SendConfirmationCodeASync(userId, request.Email,context.CancellationToken);

            return new EmailResponse
            {
                Success = true,
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }

    public override async Task<ConfirmEmailResponse> ConfirmEmail(ConfirmEmailRequest request, ServerCallContext context)
    {
        try 
        {
            await _authService.ConfirmEmailAsync(request.Email, request.Code, context.CancellationToken);

            return new ConfirmEmailResponse
            {
                Succes = true,
            };
        }
        catch (ArgumentException ex) 
        { 
            throw new RpcException(new Status(StatusCode.NotFound, ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Внутренняя ошибка сервера"));
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

    public override async Task<MeResponse> Me(MeRequest request, ServerCallContext context)
    {
        try
        {
            var token = context.RequestHeaders.GetValue("Authorization");
            
            if(token == null)
                throw new RpcException(new Status(StatusCode.Unauthenticated, string.Empty));
            
            Guid? id = (await _authService.MeAsync(token)).UserId;
            return new MeResponse
            {
                UserId = id.ToString()
            };
        }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }  
}