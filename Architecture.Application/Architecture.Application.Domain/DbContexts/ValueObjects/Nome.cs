using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Notifiable.Notifications;

namespace Architecture.Application.Domain.DbContexts.ValueObjects;

public class Nome : DomainNotifiable<Nome>, IDomainNotifiable
{
    public Nome() { }

    public string PrimeiroNome { get; set; }
    public string Sobrenome { get; set; }


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
