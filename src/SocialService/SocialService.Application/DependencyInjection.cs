using Microsoft.Extensions.DependencyInjection;
using SocialService.Application.Interfaces;
using SocialService.Application.Services;

namespace SocialService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserProfileService, UserProfileService>();
        return services;
    }
}
