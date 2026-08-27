using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlaylistService.Application.Interfaces.Repositories;
using PlaylistService.Infrastructure.Persistence;
using PlaylistService.Infrastructure.Repositories;

namespace PlaylistService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PlaylistDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), 
                npgsqlOptions => 
                {
                    npgsqlOptions.MigrationsAssembly(typeof(PlaylistDbContext).Assembly.FullName);
                    npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                }));

        services.AddScoped<IPlaylistsRepository, PlaylistsRepository>();
        
        return services;
    }
}
