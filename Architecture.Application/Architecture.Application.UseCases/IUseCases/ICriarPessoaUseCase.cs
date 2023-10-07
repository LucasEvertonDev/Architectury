using Architecture.Application.Domain.Models.Pessoa;

namespace Architecture.Application.UseCases.IUseCases
{
    public interface ICriarPessoaUseCase
    {
        Task<PessoaCriadaModel> ExecuteAsync(CriarPessoaModel param);
    }
}