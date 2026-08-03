namespace SoundAlone.Shared.Infrastructure.FileStorage;

public interface IFileStorageService
{
    Task<string> UploadAsync(Stream fileStream, string objectName, string contentType, CancellationToken ct = default);
    Task<Stream?> DownloadAsync(string objectName, CancellationToken ct = default);
    Task DeleteAsync(string objectName, CancellationToken ct = default);
    Task<string> GetPresignedUrlAsync(string objectName, int expirySeconds = 3600, CancellationToken ct = default);
}