using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModerationService.Domain.Enums;

namespace ModerationService.Domain.Entities;

[Table("user_reports")]
public class UserReport
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("user_target_id")]
    public Guid UserTargetId { get; set; }
    
    [Required]
    [Column("author_id")]
    public Guid AuthorId { get; set; }
    
    [Required]
    [Column("reason_description")]
    public required string ReasonDescription { get; set; } 
    
    [Required]
    [Column("reported_at")]
    public DateTime ReportedAt { get; set; }
    
    [Required]
    [Column("report_status")]
    public ReportStatus Status { get; set; }
}