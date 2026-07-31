using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialService.Domain.Entities;

[Table("chats")]
public class Chat
{
    [Key] 
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] 
    [Column("chat_type")]
    public string Type { get; set; } = "Dm";

    [Column("chat_name")] 
    public string? Name { get; set; } = null;

    [Column("chat_icon_url")] 
    public string? IconUrl { get; set; } = null;
    
    [Required]
    [Column("chat_created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}