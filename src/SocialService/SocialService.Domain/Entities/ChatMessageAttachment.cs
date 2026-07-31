using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialService.Domain.Entities;

[Table("chat_message_attachments")]
public class ChatMessageAttachment
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required]
    [Column("attachment_playlist_id")]
    public Guid AttachmentPlaylistId { get; set; }
}