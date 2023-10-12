using Architecture.Application.Core.Structure;
using Architecture.Application.Domain.Plugins.Cryptography;
using Architecture.Application.Domain.Plugins.JWT;
using Architectury.Infra.Plugins.FluentValidation;
using Architectury.Infra.Plugins.Hasher;
using Architectury.Infra.Plugins.TokenJWT;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Architectury.Infra.Plugins;

public static class BootstrapModule
{
    public static void RegisterPlugins(this IServiceCollection services, AppSettings configuration)
    {
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IPasswordHash, PasswordHash>();

        services.AddValidatorsFromAssemblyContaining<BaseValidator>();
    }
}
