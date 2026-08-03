using Minio;
using Minio.DataModel.Args;

namespace SoundAlone.Shared.Infrastructure.FileStorage;

public class MinioFileStorageService : IFileStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;

    public MinioFileStorageService(IMinioClient minioClient, MinioSettings settings)
    {
        _minioClient = minioClient;
        _bucketName = settings.BucketName;
    }

    public async Task<string> UploadAsync(Stream fileStream, string objectName, string contentType, CancellationToken ct = default)
    {
        await EnsureBucketExistsAsync(ct);
        
        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType), ct);

        return $"{_bucketName}/{objectName}";
    }

    public async Task<Stream?> DownloadAsync(string objectName, CancellationToken ct = default)
    {
        var memoryStream = new MemoryStream();
        await _minioClient.GetObjectAsync(new GetObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithCallbackStream(async (stream, cancellationToken) =>
            {
                await stream.CopyToAsync(memoryStream, cancellationToken);
                memoryStream.Position = 0;
            }), ct);
        
        return memoryStream.Length > 0 ? memoryStream : null;
    }

    public async Task DeleteAsync(string objectName, CancellationToken ct = default)
    {
        await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName), ct);
    }

    public async Task<string> GetPresignedUrlAsync(string objectName, int expirySeconds = 3600, CancellationToken ct = default)
    {
        return await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(objectName)
            .WithExpiry(expirySeconds));
    }

    private async Task EnsureBucketExistsAsync(CancellationToken ct)
    {
        var found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName), ct);
        if (!found)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName), ct);
        }
    }
}