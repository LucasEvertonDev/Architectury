using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Models.Pessoa;
using Architecture.Application.UseCases.IUseCases;
using Architecture.Application.UseCases.UseCases.Base;

namespace Architecture.Application.UseCases.UseCases.PessoaUseCases
{
    public class CriarPessoaUseCase : BaseUseCase<CriarPessoaModel, PessoaCriadaModel>, ICriarPessoaUseCase
    {
        public CriarPessoaUseCase(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public override Task<PessoaCriadaModel> ExecuteAsync(CriarPessoaModel param)
        {
            Notifications.Failure(new Core.Notifications.NotificationModel("business_logic", "Nome inválido."));

            var pessoa = Notifiable<Pessoa>().CriarPessoa(
                primeiroNome: param.PrimeiroNome,
                sobrenome: param.Sobrenome,
                email: param.Email,
                dataNascimento: param.DataNascimento,
                enderecoModel: param.Endereco
            );

            //if (!pessoa.IsValid())
            //{
            //    return Task.FromResult(new PessoaCriadaModel());
            //}

            if (pessoa.Nome.Sobrenome == "")
            {
                Notifications.Failure(new Core.Notifications.NotificationModel("business_logic", "Teste 2."));
            }

            if (HasError())
            {
                return Task.FromResult(new PessoaCriadaModel());
            }

            return Task.FromResult(new PessoaCriadaModel()
            {
                Message = "Filé demais"
            });
        }
    }
}
