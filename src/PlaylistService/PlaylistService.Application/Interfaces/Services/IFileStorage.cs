namespace PlaylistService.Application.Interfaces.Services;

public interface IFileStorage
{
    public Task UploadFile(string fileName, Stream stream);
    public Task DeleteFile(Guid playlistId, Guid songId);
}