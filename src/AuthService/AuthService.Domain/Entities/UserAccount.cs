using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthService.Domain.Entities;

/// <summary>
/// Аккаунт пользователя, требуется для входа в аккаунт
/// </summary>
[Table("user_accounts")]
public class UserAccount
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(128)]
    [Column("email")]
    public string Email { get; set; }

    [Column("email_verified")]
    public bool EmailVerified { get; set; } = false;
    
    [Required]
    [MaxLength(512)]
    [Column("password_hash")]
    public required string PasswordHash { get; set; }
    
    [MaxLength(128)]
    [Column("phone_number")]
    public string? PhoneNumber { get; set; } = string.Empty;
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
}