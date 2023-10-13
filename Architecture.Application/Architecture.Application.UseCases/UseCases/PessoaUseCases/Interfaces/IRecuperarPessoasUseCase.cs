using Architecture.Application.Core.Notifications;

namespace Architecture.Application.UseCases.UseCases.PessoaUseCases.Interfaces;

public interface IRecuperarPessoasUseCase
{
    Task<Result> ExecuteAsync();
}