using Architecture.Application.Domain.DbContexts.Domains.Base;

namespace Architecture.Application.Domain.DbContexts.Domains;

public class Logradouro : BaseEntity<Logradouro>
{
    public string Nome { get; set; }

    public List<Rua> Ruas { get; set; } = new List<Rua>();
    public Logradouro CriarLogradouro(string logradouro, List<Rua> ruas)
    {
        Set(logradouro => logradouro.Nome, logradouro)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("logradouro", "logradouro é obrigatório"));

        Set(logradouro => logradouro.Ruas, ruas);

        return this;
    }
}

public class Rua : BaseEntity<Rua>
{
    public string Nome { get; set; }

    public Rua CriarRua(string Rua)
    {
        Set(Rua => Rua.Nome, Rua)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("Rua", "Rua é obrigatório"));

        return this;
    }
}

