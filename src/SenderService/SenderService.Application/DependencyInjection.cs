using Microsoft.Extensions.DependencyInjection;
using SenderService.Application.Interfaces;
using SenderService.Application.Services;

namespace SenderService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddSingleton<IEmailTemplateRenderer, EmailTemplateRenderer>();

            return services;
        }
    }
}
