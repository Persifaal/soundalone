using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistService.Domain.Entities;

/// <summary>
/// Соавторы плейлиста или альбома. Для обычных плейлистов авторы могут добавлять или удалять треки
/// </summary>
[Table("playlist_authors")]
public class PlaylistAuthor
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("playlist_id")]
    public Playlist Playlist { get; set; }
    
    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }
}