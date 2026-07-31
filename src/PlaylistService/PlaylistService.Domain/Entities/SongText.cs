using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaylistService.Domain.Entities;

[Table("song_text")]
public class SongText
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [Column("text")]
    [StringLength(2048)]
    public required string Text { get; set; }
    
    [Column("approved")]
    public bool Approved { get; set; }
    
    [Required]
    [Column("text_author_id")]
    public Guid TextAuthor { get; set; }
    
    [Required]
    [Column("song_id")]
    public Guid Song { get; set; }
}