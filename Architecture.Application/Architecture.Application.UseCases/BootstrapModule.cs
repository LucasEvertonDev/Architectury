using Architecture.Application.Core.Structure;
using Architecture.Application.UseCases.UseCases.AuthUseCases;
using Architecture.Application.UseCases.UseCases.AuthUseCases.Interfaces;
using Architecture.Application.UseCases.UseCases.PessoaUseCases;
using Architecture.Application.UseCases.UseCases.PessoaUseCases.Interfaces;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Application.UseCases;

public static class BootstrapModule
{
    public static void RegisterUseCases(this IServiceCollection services, AppSettings configuration)
    {
        services.AddScoped<ICriarPessoaUseCase, CriarPessoaUseCase>();
        services.AddScoped<IRecuperarPessoasUseCase, RecuperarPessoasUseCase>();

        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();

        services.AddScoped<ICriarUsuarioUseCase, CriarUsuarioUseCase>();
        services.AddScoped<IAtualizarSenhaUseCase, AtualizarSenhaUseCase>();
        services.AddScoped<IAtualizarUsuarioUseCase, AtualizarUsuarioUseCase>();
        services.AddScoped<IBuscarUsuariosUseCase, BuscarUsuariosUseCase>();
        services.AddScoped<IExcluirUsuarioUseCase, ExcluirUsuarioUseCase>();
    }
}
