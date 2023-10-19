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

    public Endereco CriarEndereco(string cep, string estado, string cidade)
    {
        Set(endereco => endereco.Cep, cep)
            .When()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("endereco", "Cep é obrigatório"));

        Set(endereco => endereco.Estado, estado)
            .When()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("endereco", "Estado é obrigatório"));

        Set(endereco => endereco.Cidade, cidade)
            .When()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("endereco", "Cidade é obrigatório"));

        Set(endereco => endereco.Logradouro, new Logradouro().CriarLogradouro(""));

        return this;
    }
}
