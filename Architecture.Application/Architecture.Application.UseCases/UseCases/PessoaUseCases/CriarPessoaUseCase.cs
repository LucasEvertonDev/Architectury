using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.Models.Pessoa;
using Architecture.Application.UseCases.UseCases.Base;
using Architecture.Application.UseCases.UseCases.PessoaUseCases.Interfaces;

namespace Architecture.Application.UseCases.UseCases.PessoaUseCases
{
    public class CriarPessoaUseCase : BaseUseCase<CriarPessoaModel>, ICriarPessoaUseCase
    {
        private readonly ICreateRepository<Domain.DbContexts.Domains.Pessoa> _createRepository;

        public CriarPessoaUseCase(IServiceProvider serviceProvider,
            ICreateRepository<Domain.DbContexts.Domains.Pessoa> createRepository)
            : base(serviceProvider)
        {
            _createRepository = createRepository;
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

                if (pessoa.HasFailure())
                {
                    return Result.Failure<CriarPessoaUseCase>(pessoa);
                }

                var pessoaCriada = await _createRepository.CreateAsync(pessoa);

                return Result.IncludeResult(new PessoaCriadaModel()
                {
                    Message = "Filé demais"
                }); 
            });
        }
    }
}
