using PlaylistService.Domain.Entities;

namespace PlaylistService.Application.Interfaces.Repositories;

public interface IPlaylistsRepository
{
    public Task<Guid> Create(Playlist playlist);
    public Task<IEnumerable<Playlist>> GetAll();
    public Task<Playlist?> Get(Guid id);
    public Task Update(Playlist playlist);
    public Task<IEnumerable<Playlist>> GetAllOfUser(Guid userId);
}