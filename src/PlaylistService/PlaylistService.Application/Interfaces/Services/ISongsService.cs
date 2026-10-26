using PlaylistService.Domain.Entities;

namespace PlaylistService.Application.Interfaces.Services;

public interface ISongsService
{
    public Guid AddSong(Song song);
    public Song GetSong(Guid id);
}