using Architecture.Application.Core.Structure;
using Architecture.Application.Mediator.Commands.Auth.Login;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Application.UseCases;

public static class BootstrapModule
{
    public static void RegisterMediatR(this IServiceCollection services, AppSettings configuration)
    {
        ////services.AddScoped<ICriarPessoaUseCase, CriarPessoaUseCase>();
        ////services.AddScoped<IRecuperarPessoasUseCase, RecuperarPessoasUseCase>();

        ////services.AddScoped<ILoginUseCase, LoginUseCase>();
        ////services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();

        ////services.AddScoped<ICriarUsuarioUseCase, CriarUsuarioUseCase>();
        ////services.AddScoped<IAtualizarSenhaUseCase, AtualizarSenhaUseCase>();
        ////services.AddScoped<IAtualizarUsuarioUseCase, AtualizarUsuarioUseCase>();
        ////services.AddScoped<IBuscarUsuariosUseCase, BuscarUsuariosUseCase>();
        ////services.AddScoped<IExcluirUsuarioUseCase, ExcluirUsuarioUseCase>();

        services.AddMediatR(typeof(LoginCommand));
    }
}
