using Architecture.Application.Core.Structure;
using Architectury.Infra.Plugins.FluentValidation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Architectury.Infra.Plugins;

public static class BootstrapModule
{
    public static void AddPlugins(this IServiceCollection services, AppSettings configuration)
    {
        //services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<BaseValidator>();
    }
}
