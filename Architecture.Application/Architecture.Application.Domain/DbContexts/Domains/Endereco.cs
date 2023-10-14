using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.DbContexts.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Architecture.Application.Domain.DbContexts.Domains;

public class Endereco : BaseEntity<Endereco>
{
    public string Cep { get; private set; }
    public string Estado { get; private set; }
    public string Cidade { get; private set; }
    public Guid PessoaId { get; private set; }

    public Pessoa Pessoa { get; private set; } 

    public Logradouro Logradouro { get; private set; }  

    public Endereco CriarEndereco(string cep, string estado, string cidade, Logradouro logradouro)
    {
        Set(endereco => endereco.Cep, cep)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("endereco", "Cep é obrigatório"));

        Set(endereco => endereco.Estado, estado)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("endereco", "Estado é obrigatório"));

        Set(endereco => endereco.Cidade, cidade)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("endereco", "Cidade é obrigatório"));

        Set(endereco => endereco.Logradouro, logradouro);

        return this;
    }
}
