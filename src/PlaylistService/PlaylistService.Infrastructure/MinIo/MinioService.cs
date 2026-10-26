using Microsoft.Extensions.Configuration;
using Minio;
using PlaylistService.Application.Interfaces.Services;

namespace PlaylistService.Infrastructure.MinIo;

public class MinioService : IFileStorage
{
    private readonly IMinioClient? _minioClient;
    private readonly IConfiguration _configuration;

    public MinioService(IConfiguration configuration)
    {
        _configuration = configuration;
        _minioClient = new MinioClient()
            .WithEndpoint(_configuration["Minio:Endpoint"])
            .WithCredentials(_configuration["Minio:AccessKey"], _configuration["Minio:SecretKey"])
            .WithSSL(_configuration["Minio:UseSsl"]?.ToLower() == "true")
            .Build() as MinioClient;
    }
    public Task UploadFile(string fileName, Stream stream)
    {
        throw new NotImplementedException();
    }

    public Task DeleteFile(Guid playlistId, Guid songId)
    {
        throw new NotImplementedException();
    }
}