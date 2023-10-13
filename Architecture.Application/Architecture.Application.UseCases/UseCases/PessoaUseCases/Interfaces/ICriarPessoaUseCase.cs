using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Pessoa;

namespace Architecture.Application.UseCases.UseCases.PessoaUseCases.Interfaces
{
    public interface ICriarPessoaUseCase
    {
        Task<Result> ExecuteAsync(CriarPessoaModel param);
    }
}