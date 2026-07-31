using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerService.Domain.Entities;

[Table("users_last_played")]
public class UserLastPlayed
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Required]
    [Column("playlist_id")]
    public Guid PlaylistId { get; set; }
    
    [Required]
    [Column("music_id")]
    public Guid MusicId { get; set; }
}