namespace SoundAlone.Shared.Infrastructure.FileStorage;

public class MinioSettings
{
    public string Endpoint { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string BucketName { get; set; } = string.Empty; // 👈 Ключевое отличие для каждого сервиса
    public bool UseSsl { get; set; } = false;
}