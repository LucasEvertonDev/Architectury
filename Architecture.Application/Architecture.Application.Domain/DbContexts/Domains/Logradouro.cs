using Architecture.Application.Domain.DbContexts.Domains.Base;

namespace Architecture.Application.Domain.DbContexts.Domains;

public class Logradouro : BaseEntity<Logradouro>
{
    public string Nome { get; set; }

    public List<Rua> Ruas { get; set; } = new List<Rua>();
    public Logradouro CriarLogradouro(string logradouro)
    {
        Set(logradouro => logradouro.Nome, logradouro)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(new NotificationModel("logradouro", "logradouro é obrigatório"));

        var ruas = new List<Rua>()
        { 
            new Rua().CriarRua(""),
            new Rua().CriarRua("")
        };

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
            .AddFailure(new NotificationModel("Rua", "Rua é obrigatório"));

        return this;
    }
}

