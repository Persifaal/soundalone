using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlayerService.Infrastructure.Persistence;

namespace PlayerService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PlayerDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    npgsqlOptions => 
                    {
                        npgsqlOptions.MigrationsAssembly(typeof(PlayerDbContext).Assembly.FullName);
                        npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                    }
            ));
        
        return services;
    }
}
