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
    public Endereco Endereco { get; private set; }
    public List<Endereco> Enderecos { get; private set; } = new List<Endereco>();

    public Pessoa CriarPessoa(string primeiroNome, string sobrenome, string email, DateTime? dataNascimento, EnderecoModel enderecoModel)
    {
        Set(u => u.Nome, new Nome()
            .CriarNome(
                primeiroNome: primeiroNome,
                sobrenome: sobrenome
            ));

        Set(u => u.Email,email)
            .ValidateWhen()
            .IsNullOrEmpty().AddFailure(Erros.Pessoa.EmailObrigatorio)
            .IsInvalidEmail().AddFailure(Erros.Pessoa.EmailInvalido);

        Set(u => u.DataNascimento, dataNascimento);

        if(enderecoModel != null)
        {
            Set(u => u.Endereco, new Endereco()
                 .CriarEndereco(
                     cep: enderecoModel?.Cep,
                     estado: enderecoModel?.Estado,
                     cidade: enderecoModel?.Cidade
                 ));

            var endereco = new Endereco().CriarEndereco("", "", "");

            var endereco2 = new Endereco().CriarEndereco("", "", "");

            var list = new List<Endereco>() { endereco, endereco2 };

            Set(u => u.Enderecos, list);
        }

        return this;
    }
}
