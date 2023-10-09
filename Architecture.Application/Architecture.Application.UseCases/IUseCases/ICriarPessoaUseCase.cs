using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Pessoa;

namespace Architecture.Application.UseCases.IUseCases
{
    public interface ICriarPessoaUseCase
    {
        public Result Result { get; }

        Task ExecuteAsync(CriarPessoaModel param);
    }
}