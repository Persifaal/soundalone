using Microsoft.EntityFrameworkCore;

namespace PlayerService.Infrastructure.Persistence;

public class PlayerDbContext : DbContext
{
    public PlayerDbContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        
        modelBuilder.HasDefaultSchema("player_schema");
    }
}