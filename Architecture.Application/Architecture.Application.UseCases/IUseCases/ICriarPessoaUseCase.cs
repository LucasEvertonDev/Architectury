using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Pessoa;

namespace Architecture.Application.UseCases.IUseCases
{
    public interface ICriarPessoaUseCase
    {
        Task<Result> ExecuteAsync(CriarPessoaModel param);
    }
}