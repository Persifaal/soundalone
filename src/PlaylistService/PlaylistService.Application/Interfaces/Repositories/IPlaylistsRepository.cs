using PlaylistService.Domain.Entities;

namespace PlaylistService.Application.Interfaces.Repositories;

public interface IPlaylistsRepository
{
    public Task<Guid> CreateAsync(Playlist playlist);
    public Task<List<Playlist>> GetAllAsync();
    public Task<Playlist?> GetAsync(Guid id);
    public Task UpdateAsync(Playlist playlist);
    public Task<List<Playlist>> GetAllOfUserAsync(Guid userId);
    public Task AddTrackToPlaylistAsync(Guid playlistId, Guid trackId);
    public Task RemoveTrackFromPlaylistAsync(Guid playlistId, Guid trackId);
    public Task UploadCover(Guid playlistId, string path);
}