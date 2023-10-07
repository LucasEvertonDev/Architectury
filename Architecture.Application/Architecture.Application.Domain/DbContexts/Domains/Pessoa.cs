using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.DbContexts.ValueObjects;
using Architecture.Application.Domain.Models.Endereco;

namespace Architecture.Application.Domain.DbContexts.Domains;

public partial class Pessoa : BaseEntity<Pessoa>
{
    public Nome Nome { get; private set  ; }
    public string Email { get; private set; }
    public DateTime? DataNascimento { get; private set; }
    public Guid EnderecoId { get; private set; }
    public Endereco Endereco { get; private set; }

    public Pessoa CriarPessoa(string primeiroNome, string sobrenome, string email, DateTime? dataNascimento, EnderecoModel enderecoModel)
    {
        Set(pessoa => pessoa.Nome, Notifiable<Nome>()
            .CriarNome(
                primeiroNome: primeiroNome,
                sobrenome: sobrenome
            ));

        Set(pessoa => pessoa.Email, email)
            .ValidateWhen()
            .IsNullOrEmpty().AddNotification(PessoaNotifications.EmailObrigatorio)
            .IsInvalidEmail().AddNotification(PessoaNotifications.EmailInvalido);

        Set(pessoa => pessoa.DataNascimento, dataNascimento);

        Set(pessoa => pessoa.Endereco, Notifiable<Endereco>()
            .CriarEndereco(
                cep: enderecoModel.Cep,
                estado: enderecoModel.Estado,
                cidade: enderecoModel.Cidade
            ));

        return this;
    }
}
