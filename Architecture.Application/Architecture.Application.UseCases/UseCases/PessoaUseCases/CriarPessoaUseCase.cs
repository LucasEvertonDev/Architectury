using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Models.Pessoa;
using Architecture.Application.UseCases.UseCases.Base;
using Architecture.Application.UseCases.UseCases.PessoaUseCases.Interfaces;
using Architecture.Application.UseCases.UseCases.UsuarioUseCases;

namespace Architecture.Application.UseCases.UseCases.PessoaUseCases
{
    public class CriarPessoaUseCase : BaseUseCase<CriarPessoaModel>, ICriarPessoaUseCase
    {
        public CriarPessoaUseCase(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override async Task<Result> ExecuteAsync(CriarPessoaModel param)
        {
            return await OnTransactionAsync(async () =>
            {
                if (param == null)
                {
                    Result.Failure<CriarPessoaUseCase>(Erros.Pessoa.PessoaNula);
                }

                var pessoa = new Domain.DbContexts.Domains.Pessoa().CriarPessoa(
                    primeiroNome: param.PrimeiroNome,
                    sobrenome: param.Sobrenome,
                    email: param.Email,
                    dataNascimento: param.DataNascimento,
                    enderecoModel: param.Endereco
                );

                if (true)
                {
                    Result.Failure<Pessoa>((pessoa) => pessoa.Endereco.Logradouro.Nome, new FailureModel("Teste1", "Teste1"));

                    Result.Failure<Pessoa>((pessoa) => pessoa.Enderecos[1].Cidade, new FailureModel("Teste2", "Teste2"));

                    for (int i = 0; i < 2; i++)
                    {
                        Result.Failure<Pessoa>((pessoa) => pessoa.Enderecos[i].Estado, new FailureModel("Estado", $"TesteList{i}"));
                    }
                }

                if (pessoa.HasFailure())
                {
                    return Result.Failure<CriarPessoaUseCase>(pessoa);
                }

                var pessoaCriada = await unitOfWork.PessoasRepository.CreateAsync(pessoa);

                return Result.IncludeResult(new PessoaCriadaModel()
                {
                    Message = "Filé demais"
                }); 
            });
        }
    }
}
