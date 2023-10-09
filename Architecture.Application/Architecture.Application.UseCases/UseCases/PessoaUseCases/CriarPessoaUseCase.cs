using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.Models.Pessoa;
using Architecture.Application.UseCases.IUseCases;
using Architecture.Application.UseCases.UseCases.Base;

namespace Architecture.Application.UseCases.UseCases.PessoaUseCases
{
    public class CriarPessoaUseCase : BaseUseCase<CriarPessoaModel>, ICriarPessoaUseCase
    {
        public PessoaCriadaModel Retorno { get; set; } = new PessoaCriadaModel();

        private readonly ICreateRepository<Pessoa> _createRepository;

        public CriarPessoaUseCase(IServiceProvider serviceProvider,
            ICreateRepository<Pessoa> createRepository)
            : base(serviceProvider)
        {
            _createRepository = createRepository;
        }

        public override async Task ExecuteAsync(CriarPessoaModel param)
        {
            await OnTransactionAsync(async () =>
            {
                if (param == null)
                {
                    Result.Failure<CriarPessoaUseCase>(PessoaErros.PessoaNula);
                }

                var pessoa = new Pessoa().CriarPessoa(
                    primeiroNome: param.PrimeiroNome,
                    sobrenome: param.Sobrenome,
                    email: param.Email,
                    dataNascimento: param.DataNascimento,
                    enderecoModel: param.Endereco
                );

                if (pessoa.HasFailure())
                {
                    Result.Failure<CriarPessoaUseCase>(pessoa);
                    return;
                }

                var pessoaCriada = await _createRepository.CreateAsync(pessoa);

                Retorno = new PessoaCriadaModel()
                {
                    Message = "Filé demais"
                };
            });
        }
    }
}
