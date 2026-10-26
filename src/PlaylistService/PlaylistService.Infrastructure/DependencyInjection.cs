using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PlaylistService.Application.Interfaces.Repositories;
using PlaylistService.Application.Interfaces.Services;
using PlaylistService.Infrastructure.MinIo;
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
        services.AddMinio(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"]);
        

        services.AddScoped<IPlaylistsRepository, PlaylistsRepository>();
        services.AddScoped<IFileStorage, MinioService>();
        
        return services;
    }
}
