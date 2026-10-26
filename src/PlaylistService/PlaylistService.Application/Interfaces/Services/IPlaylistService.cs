using PlaylistService.Domain.Entities;

namespace PlaylistService.Application.Interfaces.Services;

public interface IPlaylistService
{
    public Task<Guid> CreatePlaylist(Playlist playlist);
    public Task DeletePlaylist(Playlist playlist);
    public Task UpdatePlaylist(Playlist playlist);
    
    public Task<List<Playlist>> GetAllAsync();
    public Task<Playlist?> GetAsync(Guid id);
    public Task<List<Playlist>> GetAllOfUserAsync(Guid userId);
    
    public Task AddSongToPlaylist(Guid playlistId, Guid songId);
    public Task RemoveSongFromPlaylist(Guid playlistId, Guid songId);
    
    public Task UploadCover(Guid playlistId, string fileName, FileStream stream);
    
}