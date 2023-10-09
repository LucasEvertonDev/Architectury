using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.DbContexts.Domains;

namespace Architecture.Application.UseCases.IUseCases;

public interface IRecuperarPessoasUseCase
{
    public Result Result { get; }

    Task ExecuteAsync();
}