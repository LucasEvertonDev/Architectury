namespace Architecture.Application.Domain.Constants;

public static class Erros
{
    public  class CredenciaisCliente
    {
        public static FailureModel IdentificacaoObrigatoria = new FailureModel("IdentificacaoObrigatoria", "Identificação é obrigatória");

        public static FailureModel ChaveObrigatoria = new FailureModel("ChaveObrigatoria", "Chave é obrigatória");

        public static FailureModel DescricaoObrigatoria = new FailureModel("DescricaoObrigatoria", "Descrição é obrigatória");
    }

    public class Pessoa
    {
        public static FailureModel NomeObrigatorio = new FailureModel("NomeObrigatorio", "Nome é obrigatório");

        public static FailureModel EmailObrigatorio = new FailureModel("EmailObrigatorio", "Email é obrigatório");

        public static FailureModel EmailInvalido = new FailureModel("EmailInválido", "Email Inválido");

        public static FailureModel PessoaNula = new FailureModel("Pessoa", "Pessoa não pode ser nulo");
    }

    public class GrupoUsuario
    {
        public static FailureModel NomeObrigatorio = new FailureModel("NomeObrigatorio", "Nome é obrigatório");

        public static FailureModel DescricaoObrigatoria = new FailureModel("DescricaoObrigatoria", "Descrição é obrigatória");
    }

    public class MapPermissoesPorGrupoUsuario
    {
        public static FailureModel PermissaoObrigatoria = new FailureModel("PermissaoObrigatoria", "Permissão é obrigatória");

        public static FailureModel GrupoUsuarioObrigatorio = new FailureModel("GrupoUsuarioObrigatorio", "Grupo de usuário é obrigatório");
    }

    public class Permissao
    {
        public static FailureModel NomeObrigatorio = new FailureModel("NomeObrigatorio", "Nome é obrigatório");
    }

    public class Usuario
    {
        public static FailureModel NomeObrigatorio = new FailureModel("NomeObrigatorio", "Nome é obrigatório");

        public static FailureModel EmailObrigatorio = new FailureModel("EmailObrigatorio", "Email é obrigatório");

        public static FailureModel EmailInvalido = new FailureModel("EmailInválido", "Email Inválido");

        public static FailureModel UsernameObrigatorio = new FailureModel("UsernameObrigatorio", "Username é obrigatória");

        public static FailureModel PasswordObrigatorio = new FailureModel("PasswordObrigatorio", "Senha é obrigatória");

        public static FailureModel PasswordHashObrigatorio = new FailureModel("PasswordHash", "Hash obrigatório");

        public static FailureModel GrupoUsuarioObrigatorio = new FailureModel("GrupoUsuarioObrigatorio", "Grupo de Usuário é obrigatório");

        public static FailureModel DataUltimoAcessoInvalida = new FailureModel("DataUltimoAcessoInvalida", "Data de último acesso inválida");

        public static FailureModel GrupoUsuarioInvalido = new FailureModel("GrupoUsuarioInvalido", "Grupo de usuário inválido");

        public static FailureModel PasswordLenght = new FailureModel("PasswordLenght", "Senha deve ter no máximo seis caracteres");
    }

    public class Business
    {
        public static FailureModel CrendenciaisClienteInvalida = new FailureModel("CrendenciaisClienteInvalida", "Credencias de clinete inválidas");

        public static FailureModel UsernamePasswordInvalidos = new FailureModel("UsernamePasswordInvalidos", "Username ou senha inválidos");

        public static FailureModel RefreshTokenInvalido = new FailureModel("RefreshTokenInvalido", "Refresh token inválido");

        public static FailureModel UsernameExistente = new FailureModel("UsernameCadastrado", "Já existe um usuário cadastrado para o username informado.");

        public static FailureModel EmailExistente = new FailureModel("EmailExistente", "Já existe um usuário cadastrado para o email informado.");

        public static FailureModel UsuarioInexistente = new FailureModel("UsuarioInexistente", "Não foi possível encontrar o usuário para os parâmetros informados ");
    }
}
