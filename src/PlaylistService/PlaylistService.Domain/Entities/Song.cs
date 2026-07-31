using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistService.Domain.Entities;

[Table("song")]
public class Song
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [Column("name")]
    [MaxLength(256)]
    public required string Name { get; set; }
    
    [Required]
    [Column("original_album")]
    public required Playlist OriginalAlbum { get; set; }
    
    [Required]
    [Column("published_at")]
    public DateTime PublishedAt { get; set; }

    [Column("explicit")] 
    public bool Explicit { get; set; } = false;
    
    [Required]
    [Column("url")]
    public required string Link { get; set; }
}