using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistService.Domain.Entities;

/// <summary>
/// Автор трека, у трека может быть несколько авторов
/// </summary>
public class SongAuthor
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Required]
    [Column("song_id")]
    public Guid SongId { get; set; }
}