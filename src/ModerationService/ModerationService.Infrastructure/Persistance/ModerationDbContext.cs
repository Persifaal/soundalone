using Microsoft.EntityFrameworkCore;
using ModerationService.Domain.Entities;

namespace ModerationService.Infrastructure.Persistance;

public class ModerationDbContext : DbContext
{
    public ModerationDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<UserReport> UserReports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasDefaultSchema("moderation_schema");
    }
}