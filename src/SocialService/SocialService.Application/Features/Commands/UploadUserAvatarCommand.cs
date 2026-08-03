using MediatR;

namespace SocialService.Application.Features.Commands;

public record UploadUserAvatarCommand(
    Guid UserId,
    Stream FileStream,
    string FileName,
    string ContentType,
    long FileSize
) : IRequest<UploadAvatarResponse>;

public record UploadAvatarResponse(string AvatarUrl);