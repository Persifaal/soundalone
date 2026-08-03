using FluentValidation;
using SocialService.Application.Features.Commands;

namespace SocialService.Application.Features.Validators;

public class UploadAvatarValidator : AbstractValidator<UploadUserAvatarCommand>
{
    private static readonly HashSet<string> AllowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg",
        "image/png",
        "image/webp"
    };

    private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

    public UploadAvatarValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.FileStream)
            .NotNull()
            .Must(stream => stream.Length > 0)
            .WithMessage("Файл пуст.");

        RuleFor(x => x.FileSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(MaxFileSizeBytes)
            .WithMessage($"Размер файла не должен превышать {MaxFileSizeBytes / 1024 / 1024} МБ.");

        RuleFor(x => x.ContentType)
            .Must(ct => AllowedContentTypes.Contains(ct))
            .WithMessage("Разрешены только изображения: JPEG, PNG, WebP.");

        RuleFor(x => x.FileName)
            .NotEmpty()
            .MaximumLength(255);
    }
}