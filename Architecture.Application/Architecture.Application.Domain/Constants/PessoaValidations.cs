namespace Architecture.Application.Domain.Constants;

public class PessoaNotifications
{
    public static NotificationModel NomeObrigatorio = new NotificationModel("NomeObrigatorio", "Nome é obrigatório");

    public static NotificationModel EmailObrigatorio = new NotificationModel("EmailObrigatorio", "Email é obrigatório");

    public static NotificationModel EmailInvalido = new NotificationModel("EmailInválido", "Email Inválido");

}