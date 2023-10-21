using Architecture.Application.Domain.DbContexts.Domains.Base;

namespace Architecture.Application.Domain.DbContexts.Domains;

public class Endereco : BaseEntity<Endereco>
{
    public string Cep { get; private set; }
    public string Estado { get; private set; }
    public string Cidade { get; private set; }
    public Guid PessoaId { get; private set; }
    public Pessoa Pessoa { get; private set; } 
    public Logradouro Logradouro { get; private set; }  

    public Endereco CriarEndereco(string cep, string estado, string cidade)
    {
        Set(Cep => cep)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("endereco", "Cep é obrigatório"));

        Set(Estado => estado)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("endereco", "Estado é obrigatório"));

        Set(Cidade => cidade)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("endereco", "Cidade é obrigatório"));

        Set(Logradouro => new Logradouro().CriarLogradouro(""));

        return this;
    }
}
