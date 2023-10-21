using Architecture.Application.Core.Notifications.Annotations;
using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.Constants.Validations.Domains;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.Enuns;

namespace Architecture.Application.Domain.DbContexts.Domains;

public class Usuario : BaseEntity<Usuario>
{
    [CustomRequired(ErrorMessage = UsuarioValidations.UsernameRequired.Message, ErrorCode = UsuarioValidations.UsernameRequired.Code)]
    public string Username { get; private set; }

    [CustomRequired(ErrorMessage = UsuarioValidations.PasswordRequired.Message, ErrorCode = UsuarioValidations.PasswordRequired.Code)]
    public string Password { get; private set; }

    [CustomRequired(ErrorMessage = UsuarioValidations.PasswordHashRequired.Message, ErrorCode = UsuarioValidations.PasswordHashRequired.Code)]
    public string PasswordHash { get; private set; }

    [CustomRequired(ErrorMessage = UsuarioValidations.GrupoUsuarioIdRequired.Message, ErrorCode = UsuarioValidations.GrupoUsuarioIdRequired.Code)]
    public Guid GrupoUsuarioId { get; private set; }

    [CustomRequired(ErrorMessage = UsuarioValidations.NomeRequired.Message, ErrorCode = UsuarioValidations.NomeRequired.Code)]
    public string Nome { get; private set; }

    [CustomRequired(ErrorMessage = UsuarioValidations.EmailRequired.Message, ErrorCode = UsuarioValidations.EmailRequired.Code)]
    public string Email { get; private set; }
    public DateTime UltimoAcesso { get; private set; }
    public GrupoUsuario GrupoUsuario {  get; private set; }

    public Usuario CriarUsuario(string username, string password, string passwordHash, GrupoUsuario grupoUsuario, string nome, string email)
    {
        Set(Username => username);

        Set(Password => password);

        Set(PasswordHash => passwordHash);

        Set(GrupoUsuario => grupoUsuario);

        Set(Nome => nome);

        Set(Email => email);

        Set(Situacao => (int)ESituacao.Ativo);

        return this;
    }

    public Usuario AtualizaUsuario(string username, string email, string nome, GrupoUsuario grupoUsuario)
    {
        Set(Username => username);

        Set(GrupoUsuario => grupoUsuario);

        Set(Nome => nome);

        Set(Email => email);

        return this;
    }

    public Usuario AtualizaSenhaUsuario(string password, string passwordHash)
    {
        Set(Password => password);

        Set(PasswordHash => passwordHash);

        return this;
    }

    public void RegistraUltimoAcesso()
    {
        this.UltimoAcesso = DateTime.Now;
    }
}
