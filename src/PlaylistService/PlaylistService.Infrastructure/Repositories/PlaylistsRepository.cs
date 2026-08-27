using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlaylistService.Application.Interfaces.Repositories;
using PlaylistService.Domain.Entities;
using PlaylistService.Infrastructure.Persistence;

namespace PlaylistService.Infrastructure.Repositories;

public class PlaylistsRepository : IPlaylistsRepository
{
    private readonly ILogger<PlaylistsRepository> _logger;
    private readonly PlaylistDbContext _context;

    public PlaylistsRepository(ILogger<PlaylistsRepository> logger, PlaylistDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Guid> CreateAsync(Playlist playlist)
    {
        playlist.Id = Guid.NewGuid();
        
        await _context.Playlists.AddAsync(playlist);
        await _context.SaveChangesAsync();
        
        return playlist.Id;
    }

    public async Task<List<Playlist>> GetAllAsync()
    {
        return await _context.Playlists.ToListAsync();
    }

    public async Task<Playlist?> GetAsync(Guid id)
    {
        return await _context.Playlists.FirstAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Playlist playlist)
    {
        var playlistToUpdate = _context.Playlists.First(x => x.Id == playlist.Id);
        
        playlistToUpdate.Name = playlist.Name;
        playlistToUpdate.Description = playlist.Description;
        
        _context.Playlists.Update(playlistToUpdate);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Playlist>> GetAllOfUserAsync(Guid userId)
    {
        return await _context.Playlists
            .Where(p =>
                p.Owner == userId ||
                _context.PlaylistAuthors.Any(pa =>
                    pa.Playlist.Id == p.Id &&
                    pa.UserId == userId))
            .ToListAsync();
    }

    public async Task AddTrackToPlaylistAsync(Guid playlistId, Guid trackId)
    {
        var playlist = await _context.Playlists.FirstAsync(x => x.Id == playlistId);

        if (playlist == null)
        {
            throw new Exception("Playlist not found");
        }
        
        var track = await _context.Songs.FirstAsync(x => x.Id == trackId);

        if (track == null)
        {
            throw new Exception("Track not found");
        }
        
        var participant = new PlaylistSongParticipant()
        {
            Id = Guid.NewGuid(),
            PlaylistId = playlistId,
            SongId = trackId,
        };
        
        await _context.PlaylistSongParticipants.AddAsync(participant);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveTrackFromPlaylistAsync(Guid playlistId, Guid trackId)
    {
        var participant = await _context.PlaylistSongParticipants.FirstAsync(x => x.PlaylistId == playlistId && x.Id == trackId);
        
        _context.PlaylistSongParticipants.Remove(participant);
        await _context.SaveChangesAsync();
    }
}