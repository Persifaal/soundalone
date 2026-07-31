using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialService.Domain.Entities;

[Table("UserProfile")]
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
    
    [Column("role")]
    [MaxLength(50)]
    public string Role { get; set; } = "User";
    
    [Column("bio")]
    [MaxLength(500)]
    public string? Bio { get; set; }
    
    [Column("related_account")]
    public Guid RelatedAccount { get; set; }
}