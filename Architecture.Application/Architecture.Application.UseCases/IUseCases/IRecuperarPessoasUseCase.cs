using Architecture.Application.Core.Notifications;

namespace Architecture.Application.UseCases.IUseCases;

public interface IRecuperarPessoasUseCase
{
    Task<Result> ExecuteAsync();
}