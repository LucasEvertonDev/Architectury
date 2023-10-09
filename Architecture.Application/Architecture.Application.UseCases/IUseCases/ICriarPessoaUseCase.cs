using Architecture.Application.Domain.Models.Pessoa;

namespace Architecture.Application.UseCases.IUseCases
{
    public interface ICriarPessoaUseCase
    {
        PessoaCriadaModel Retorno { get; set; }

        Task ExecuteAsync(CriarPessoaModel param);
    }
}