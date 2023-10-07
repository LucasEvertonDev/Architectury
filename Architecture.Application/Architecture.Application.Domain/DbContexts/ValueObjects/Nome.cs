using Architecture.Application.Core.Notifications.Notifiable;
using Architecture.Application.Domain.DbContexts.ValueObjects.Base;

namespace Architecture.Application.Domain.DbContexts.ValueObjects;

public record Nome : ValueObjectRecord<Nome>
{
    public Nome() { }

    public string PrimeiroNome { get; private set; }
    public string Sobrenome { get; private set; }


    public string NomeCompleto() => string.Concat(PrimeiroNome, " ", Sobrenome);

    public Nome CriarNome(string primeiroNome, string sobrenome)
    {
        Set(nome => nome.PrimeiroNome, primeiroNome)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddNotification(new NotificationModel("PRIMEIRO_NOME", "Primeiro nome é obrigatório"));

        Set(nome => nome.Sobrenome, sobrenome)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddNotification(new NotificationModel("SOBRENOME", "SobreNome é obrigatório"));
       
        return this;
    }
}
