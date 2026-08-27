using Microsoft.Extensions.Logging;
using PlaylistService.Application.Interfaces.Repositories;
using PlaylistService.Application.Interfaces.Services;
using PlaylistService.Domain.Entities;

namespace PlaylistService.Application.Services;

public class PlaylistService : IPlaylistService
{
    private readonly ILogger<PlaylistService> _logger;
    private readonly IPlaylistsRepository _playlistsRepository;

    public PlaylistService(ILogger<PlaylistService> logger, IPlaylistsRepository playlistsRepository)
    {
        _logger = logger;
        _playlistsRepository = playlistsRepository;
    }


    public Task CreatePlaylist(Playlist playlist)
    {
        return _playlistsRepository.CreateAsync(playlist);
    }

    public Task DeletePlaylist(Playlist playlist)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePlaylist(Playlist playlist)
    {
        return _playlistsRepository.UpdateAsync(playlist);
    }

    public Task<List<Playlist>> GetAllAsync()
    {
        return _playlistsRepository.GetAllAsync();
    }

    public Task<Playlist?> GetAsync(Guid id)
    {
        return _playlistsRepository.GetAsync(id);
    }

    public Task<List<Playlist>> GetAllOfUserAsync(Guid userId)
    {
        return _playlistsRepository.GetAllOfUserAsync(userId);
    }

    public Task AddSongToPlaylist(Guid playlistId, Guid songId)
    {
        return _playlistsRepository.AddTrackToPlaylistAsync(playlistId, songId);
    }

    public Task RemoveSongFromPlaylist(Guid playlistId, Guid songId)
    {
        return _playlistsRepository.RemoveTrackFromPlaylistAsync(playlistId, songId);
    }
}