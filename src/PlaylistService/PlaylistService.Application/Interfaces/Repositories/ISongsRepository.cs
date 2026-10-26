using PlaylistService.Domain.Entities;

namespace PlaylistService.Application.Interfaces.Repositories;

public interface ISongsRepository
{
    public Guid CreateSong(Guid playlistId, Guid songId, string title, string artist);
    public Song GetSong(Guid songId);
    public IEnumerable<Song> GetSongs(Guid playlistId);
    
}