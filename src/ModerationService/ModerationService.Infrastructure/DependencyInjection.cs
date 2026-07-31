using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModerationService.Infrastructure.Persistance;

namespace ModerationService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ModerationDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions => 
                {
                    npgsqlOptions.MigrationsAssembly(typeof(ModerationDbContext).Assembly.FullName);
                    npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                }
                ));
        
        return services;
    }
}
