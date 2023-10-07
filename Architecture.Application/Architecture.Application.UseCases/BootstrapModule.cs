using Architecture.Application.Core.Structure;
using Architecture.Application.UseCases.IUseCases;
using Architecture.Application.UseCases.UseCases.PessoaUseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Architecture.Application.UseCases;

public static class BootstrapModule
{
    public static void RegisterUseCases(this IServiceCollection services, AppSettings configuration)
    {
        services.AddScoped<ICriarPessoaUseCase, CriarPessoaUseCase>();
        services.AddScoped<IRecuperarPessoasUseCase, RecuperarPessoasUseCase>();
    }
}
