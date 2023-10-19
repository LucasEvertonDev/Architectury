using Architecture.Application.Domain.Constants;
using Architecture.Application.Domain.DbContexts.Domains.Base;
using Architecture.Application.Domain.Enuns;
using Newtonsoft.Json.Linq;

namespace Architecture.Application.Domain.DbContexts.Domains;

public class Usuario : BaseEntity<Usuario>
{
    public string Username
    {
        get;
        private set;
    }
    public string Password { get; private set; }
    public string PasswordHash { get; private set; }
    public Guid GrupoUsuarioId { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public DateTime UltimoAcesso { get; private set; }
    public GrupoUsuario GrupoUsuario {  get; private set; }

    public Usuario CriarUsuario(string username, string password, string passwordHash, GrupoUsuario grupoUsuario, string nome, string email)
    {
        RuleFor(usuario => usuario.Password, password).When().IsNullOrEmpty().AddFailure(Erros.Usuario.PasswordObrigatorio);
        RuleFor(usuario => usuario.PasswordHash, passwordHash).When().IsNullOrEmpty().AddFailure(Erros.Usuario.PasswordHashObrigatorio);
        RuleFor(usuario => usuario.GrupoUsuario, grupoUsuario).When().IsNullOrEmpty().AddFailure(Erros.Usuario.GrupoUsuarioInvalido);
        RuleFor(usuario => usuario.Nome, nome).When().IsNullOrEmpty().AddFailure(Erros.Usuario.NomeObrigatorio);
        RuleFor(usuario => usuario.Email, email).When().IsNullOrEmpty().AddFailure(Erros.Usuario.EmailObrigatorio);

        if (string.IsNullOrEmpty(username))
        {
            Result.Failure<Usuario>(usuario => usuario.Username, Erros.Usuario.UsernameObrigatorio);
        }

        this.Username = username;
        this.Password = password;
        this.PasswordHash = passwordHash;
        this.Situacao = (int)ESituacao.Ativo;
        this.Nome = nome;
        this.Email = email;

        return this;
    }

    private void Set2(Func<object, string> value)
    {
        throw new NotImplementedException();
    }

    public Usuario AtualizaUsuario(string username, string email, string nome, GrupoUsuario grupoUsuario)
    {
        Set(usuario => usuario.Username, username)
            .When()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.UsernameObrigatorio);

        Set(usuario => usuario.GrupoUsuario, grupoUsuario)
            .When()
            .IsNull()
            .AddFailure(Erros.Usuario.GrupoUsuarioInvalido);

        Set(usuario => usuario.Nome, nome)
            .When()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.NomeObrigatorio);

        Set(usuario => usuario.Email, email)
            .When()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.EmailObrigatorio);

        return this;
    }

    public Usuario AtualizaSenhaUsuario(string password, string passwordHash)
    {
        Set(usuario => usuario.Password, password)
          .When()
          .IsNullOrEmpty()
          .AddFailure(Erros.Usuario.PasswordObrigatorio);

        Set(usuario => usuario.PasswordHash, passwordHash)
            .When()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.PasswordHashObrigatorio);

        return this;
    }

    public void RegistraUltimoAcesso()
    {
        this.UltimoAcesso = DateTime.Now;
    }
}
