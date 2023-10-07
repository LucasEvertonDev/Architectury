using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.DbContexts.Repositorys.Base;
using Architecture.Application.Domain.Models.Pessoa;
using Architecture.Application.UseCases.IUseCases;
using Architecture.Application.UseCases.UseCases.Base;

namespace Architecture.Application.UseCases.UseCases.PessoaUseCases
{
    public class CriarPessoaUseCase : BaseUseCase<CriarPessoaModel, PessoaCriadaModel>, ICriarPessoaUseCase
    {
        private readonly ICreateRepository<Pessoa> _createRepository;
        private readonly ICreateRepository<Endereco> _createEndereco;

        public CriarPessoaUseCase(IServiceProvider serviceProvider,
            ICreateRepository<Pessoa> createRepository,
            ICreateRepository<Endereco> createEndereco)
            : base(serviceProvider)
        {
            _createRepository = createRepository;
            _createEndereco = createEndereco;
        }

        public override async Task<PessoaCriadaModel> ExecuteAsync(CriarPessoaModel param)
        {
            return await OnTransactionAsync(async () =>
            {
                var pessoa = Notifiable<Pessoa>().CriarPessoa(
                    primeiroNome: param.PrimeiroNome,
                    sobrenome: param.Sobrenome,
                    email: param.Email,
                    dataNascimento: param.DataNascimento,
                    enderecoModel: param.Endereco
                );

                if (!pessoa.IsValid())
                {
                    return new PessoaCriadaModel();
                }

                await _createEndereco.CreateAsync(pessoa.Endereco);  

                var pp = await _createRepository.CreateAsync(pessoa);

                return new PessoaCriadaModel()
                {
                    Message = "Filé demais"
                };
            });
        }
    }
}
