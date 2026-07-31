using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PlayerService.Domain.Enums;

namespace PlayerService.Domain.Entities;

[Table("player_options")]
public class PlayerOptions
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("shuffle")] 
    public ShuffleOptions Shuffle { get; set; } = ShuffleOptions.Disabled;
    
    [Column("repeat")]
    public RepeatOptions Repeat { get; set; } = RepeatOptions.Disabled;
}