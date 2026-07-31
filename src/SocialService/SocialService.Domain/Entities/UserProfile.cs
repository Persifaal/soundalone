using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SocialService.Domain.Enums;

namespace SocialService.Domain.Entities;

[Table("user_profile")]
public class UserProfile
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("user_tag")] 
    [MaxLength()]
    public string UserTag { get; set; }
    
    [Required]
    [Column("display_name")]
    public string DisplayName { get; set; }
    
    [Column("avatar_url")]
    [MaxLength(500)]
    public string? AvatarUrl { get; set; }
    
    [Column("banner_url")]
    [MaxLength(500)]
    public string? BannerUrl { get; set; }

    [Column("is_musician")] 
    public bool IsMusician { get; set; } = false;
    
    [Column("musician_type")]
    public MusicianType? MusicianType { get; set; }
    
    [Column("role")]
    public UserRole UserRole { get; set; }
    
    [Column("bio")]
    [MaxLength(500)]
    public string? Bio { get; set; }
    
    [Column("related_account")]
    public Guid RelatedAccount { get; set; }
}