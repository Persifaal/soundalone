using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlaylistService.Domain.Enums;

namespace PlaylistService.Domain.Entities;

[Table("playlists")]
public class Playlist
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [Column("name")]
    public required string Name { get; set; }
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Required]
    [Column("playlist_type")]
    public PlaylistType PlaylistType { get; set; }
    
    [Required]
    [Column("access_type")]
    public PlaylistAccess AccessType { get; set; } = PlaylistAccess.Private;
    
    /// <summary>
    /// Оригинальный создатель плейлиста. Может изменять информацию о плейлисте и удалить его
    /// </summary>
    [Required]
    [Column("owner")]
    public Guid Owner { get; set; }
    
    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    
    [Column("cover_path")]
    public string CoverPath { get; set; } = string.Empty;
}