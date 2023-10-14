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

    public Pessoa CriarPessoa(string primeiroNome, string sobrenome, string email, DateTime? dataNascimento, Endereco endereco, List<Endereco> enderecos = null)
    {
        Set(pessoa => pessoa.Nome, new Nome()
            .CriarNome(
                primeiroNome: primeiroNome,
                sobrenome: sobrenome
            ));

        Set(pessoa => pessoa.Email, email)
            .ValidateWhen()
            .IsNullOrEmpty().AddFailure(Erros.Pessoa.EmailObrigatorio)
            .IsInvalidEmail().AddFailure(Erros.Pessoa.EmailInvalido);

        Set(pessoa => pessoa.DataNascimento, dataNascimento);

        Set(pessoa => pessoa.Endereco, endereco)
            .ValidateWhen()
            .IsNull()
            .AddFailure(Erros.Pessoa.EnderecoEObrigatorio);

        Set(pessoa => pessoa.Enderecos, enderecos)
            .ValidateWhen()
            .IsNull()
            .AddFailure(Erros.Pessoa.EnderecosEObrigatorio);

        return this;
    }
}
