using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistService.Domain.Entities;

[Table("playlist_song_participants")]
public class PlaylistSongParticipant
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("playlist_id")]
    public Guid PlaylistId { get; set; }
    
    [Required]
    [Column("song_id")]
    public Guid SongId { get; set; }
}