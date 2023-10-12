using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.Enuns;

namespace Architecture.Application.Domain.DbContexts.Domains;

public class Usuario : BaseEntity<Usuario>
{
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string PasswordHash { get; private set; }
    public Guid GrupoUsuarioId { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public DateTime UltimoAcesso { get; private set; }
    public GrupoUsuario GrupoUsuario {  get; private set; }

    public Usuario CriarUsuario(string username, string password, string passwordHash, GrupoUsuario grupoUsuario, string nome, string email)
    {
        Set(usuario => usuario.Username, username)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.UsernameObrigatorio);

        Set(usuario => usuario.Password, password)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.PasswordObrigatorio);

        Set(usuario => usuario.PasswordHash, passwordHash)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.PasswordHashObrigatorio);

        Set(usuario => usuario.GrupoUsuario, grupoUsuario)
            .ValidateWhen()
            .IsNull()
            .AddFailure(Erros.Usuario.GrupoUsuarioInvalido);

        Set(usuario => usuario.Nome, nome)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.NomeObrigatorio);

        Set(usuario => usuario.Email, email)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.EmailObrigatorio);

        Set(usuario => usuario.Situacao, (int)ESituacao.Ativo);

        return this;
    }

    public void RegistraUltimoAcesso()
    {
        this.UltimoAcesso = DateTime.Now;
    }
}
