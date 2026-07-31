using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialService.Domain.Entities;

[Table("chat_members")]
public class ChatMembers
{
    [Key] 
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [Column("member_id")]
    public required UserProfile Member { get; set; }
    
    [Required]
    [Column("chat_id")]
    public required Chat Chat { get; set; }
}