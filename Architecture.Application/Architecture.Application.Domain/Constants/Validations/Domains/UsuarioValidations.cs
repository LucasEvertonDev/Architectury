namespace Architecture.Application.Domain.Constants.Validations.Domains;

public class UsuarioValidations
{
    public record UsernameRequired
    {
        public const string Code = "UsernameRequired";

        public const string Message = "Username é obrigatório";
    }

    public record PasswordRequired
    {
        public const string Code = "PasswordRequired";

        public const string Message = "Password é obrigatório";
    }
    
    public record PasswordHashRequired
    {
        public const string Code = "PasswordHash";

        public const string Message = "PasswordHash é obrigatório";
    }

    public record GrupoUsuarioIdRequired
    {
        public const string Code = "GrupoUsuarioIdRequired";

        public const string Message = "GrupoUsuarioId é obrigatório";
    }

    public record NomeRequired
    {
        public const string Code = "NomeRequired";

        public const string Message = "Nome é obrigatório";
    }

    public record EmailRequired
    {
        public const string Code = "EmailRequired";

        public const string Message = "Email é obrigatório";
    }
}
