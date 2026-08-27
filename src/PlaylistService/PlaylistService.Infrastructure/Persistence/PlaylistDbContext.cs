using Microsoft.EntityFrameworkCore;
using PlaylistService.Domain.Entities;

namespace PlaylistService.Infrastructure.Persistence;

public class PlaylistDbContext : DbContext
{
    public PlaylistDbContext(DbContextOptions options) : base(options) {}

    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistAuthor> PlaylistAuthors { get; set; }
    public DbSet<PlaylistSongParticipant> PlaylistSongParticipants { get; set; }
    
    public DbSet<Song> Songs { get; set; }
    public DbSet<SongAuthor> SongAuthors { get; set; }
    
    public DbSet<SongText> SongTexts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        
        modelBuilder.HasDefaultSchema("playlists_schema");
    }
}