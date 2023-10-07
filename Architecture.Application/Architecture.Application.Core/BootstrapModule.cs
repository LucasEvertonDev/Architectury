using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Application.Core;

public static class BootstrapModule
{
    public static void RegisterCoreServices(this IServiceCollection services, AppSettings configuration)
    {
        services.AddScoped<NotificationContext>();
    }
}
