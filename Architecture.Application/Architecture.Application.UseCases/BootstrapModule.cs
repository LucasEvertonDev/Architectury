using Architecture.Application.Core.Structure;
using Architecture.Application.Mediator.Commands.Auth.Login;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Application.UseCases;

public static class BootstrapModule
{
    public static void RegisterMediatR(this IServiceCollection services, AppSettings configuration)
    {
        services.AddMediatR(typeof(LoginCommand));
    }
}
