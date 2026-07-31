using Microsoft.EntityFrameworkCore;
using SocialService.Domain.Entities;

namespace SocialService.Infrastructure.Persistence;

public class SocialDbContext : DbContext
{
    protected SocialDbContext(DbContextOptions options) : base(options) {}

    public DbSet<UserProfile> UserProfiles { get; set; }
    
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatMembers> ChatMembers { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<ChatMessageAttachment> ChatMessageAttachments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("social_schema");
        
        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasIndex(e => e.UserTag).IsUnique();
        });
    }
    
}