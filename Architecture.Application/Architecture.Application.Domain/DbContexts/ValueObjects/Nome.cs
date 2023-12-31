﻿using Architecture.Application.Domain.DbContexts.ValueObjects.Base;

namespace Architecture.Application.Domain.DbContexts.ValueObjects;

public class Nome : ValueObject<Nome>
{
    public Nome() { }

    public string PrimeiroNome { get; set; }
    public string Sobrenome { get; set; }


    public string NomeCompleto() => string.Concat(PrimeiroNome, " ", Sobrenome);

    public Nome CriarNome(string primeiroNome, string sobrenome)
    {
        Set(PrimeiroNome => primeiroNome)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("PRIMEIRO_NOME", "Primeiro nome é obrigatório"));

        Set(Sobrenome => sobrenome)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(new FailureModel("SOBRENOME", "SobreNome é obrigatório"));
       
        return this;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return PrimeiroNome;
        yield return Sobrenome;
    }
}
