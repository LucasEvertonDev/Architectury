namespace Architecture.Application.Domain.Constants;

public static class Erros
{
    public  class CredenciaisCliente
    {
        public static NotificationModel IdentificacaoObrigatoria = new NotificationModel("IdentificacaoObrigatoria", "Identificação é obrigatória");

        public static NotificationModel ChaveObrigatoria = new NotificationModel("ChaveObrigatoria", "Chave é obrigatória");

        public static NotificationModel DescricaoObrigatoria = new NotificationModel("DescricaoObrigatoria", "Descrição é obrigatória");
    }

    public class Pessoa
    {
        public static NotificationModel NomeObrigatorio = new NotificationModel("NomeObrigatorio", "Nome é obrigatório");

        public static NotificationModel EmailObrigatorio = new NotificationModel("EmailObrigatorio", "Email é obrigatório");

        public static NotificationModel EmailInvalido = new NotificationModel("EmailInválido", "Email Inválido");

        public static NotificationModel PessoaNula = new NotificationModel("Pessoa", "Pessoa não pode ser nulo");
    }

    public class GrupoUsuario
    {
        public static NotificationModel NomeObrigatorio = new NotificationModel("NomeObrigatorio", "Nome é obrigatório");

        public static NotificationModel DescricaoObrigatoria = new NotificationModel("DescricaoObrigatoria", "Descrição é obrigatória");
    }

    public class MapPermissoesPorGrupoUsuario
    {
        public static NotificationModel PermissaoObrigatoria = new NotificationModel("PermissaoObrigatoria", "Permissão é obrigatória");

        public static NotificationModel GrupoUsuarioObrigatorio = new NotificationModel("GrupoUsuarioObrigatorio", "Grupo de usuário é obrigatório");
    }

    public class Permissao
    {
        public static NotificationModel NomeObrigatorio = new NotificationModel("NomeObrigatorio", "Nome é obrigatório");
    }

    public class Usuario
    {
        public static NotificationModel NomeObrigatorio = new NotificationModel("NomeObrigatorio", "Nome é obrigatório");

        public static NotificationModel EmailObrigatorio = new NotificationModel("EmailObrigatorio", "Email é obrigatório");

        public static NotificationModel EmailInvalido = new NotificationModel("EmailInválido", "Email Inválido");

        public static NotificationModel UsernameObrigatorio = new NotificationModel("UsernameObrigatorio", "Username é obrigatória");

        public static NotificationModel PasswordObrigatorio = new NotificationModel("PasswordObrigatorio", "Senha é obrigatória");

        public static NotificationModel PasswordHashObrigatorio = new NotificationModel("PasswordHash", "Hash obrigatório");

        public static NotificationModel GrupoUsuarioObrigatorio = new NotificationModel("GrupoUsuarioObrigatorio", "Grupo de Usuário é obrigatório");

        public static NotificationModel DataUltimoAcessoInvalida = new NotificationModel("DataUltimoAcessoInvalida", "Data de último acesso inválida");

        public static NotificationModel GrupoUsuarioInvalido = new NotificationModel("GrupoUsuarioInvalido", "Grupo de usuário inválido");
    }

    public class Business
    {
        public static NotificationModel CrendenciaisClienteInvalida = new NotificationModel("CrendenciaisClienteInvalida", "Credencias de clinete inválidas");

        public static NotificationModel UsernamePasswordInvalidos = new NotificationModel("UsernamePasswordInvalidos", "Username ou senha inválidos");

        public static NotificationModel RefreshTokenInvalido = new NotificationModel("RefreshTokenInvalido", "Refresh token inválido");

        public static NotificationModel UsernameExistente = new NotificationModel("UsernameCadastrado", "Já existe um usuário cadastrado para o username informado.");

        public static NotificationModel EmailExistente = new NotificationModel("EmailExistente", "Já existe um usuário cadastrado para o email informado.");

    }
}
