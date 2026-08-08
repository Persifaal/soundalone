using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialService.Application.Interfaces;
using SocialService.Infrastructure.Persistence;
using SocialService.Infrastructure.Repositories;

namespace SocialService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SocialDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                npgsqlOptions => 
                {
                    npgsqlOptions.MigrationsAssembly(typeof(SocialDbContext).Assembly.FullName);
                    npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "public");
                }));

        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        
        return services;
    }
}
