﻿using Architecture.Application.Domain.Constants;
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
        Set(u => u.Username, username)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.UsernameObrigatorio);

        Set(u => u.Password, password)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.PasswordObrigatorio);

        Set(u => u.PasswordHash,passwordHash)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.PasswordHashObrigatorio);

        Set(u => u.GrupoUsuario, grupoUsuario)
            .ValidateWhen()
            .IsNull()
            .AddFailure(Erros.Usuario.GrupoUsuarioInvalido);

        Set(u => u.Nome, nome)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.NomeObrigatorio);

        Set(u => u.Email, email)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.EmailObrigatorio);

        Set(u => u.Situacao , (int)ESituacao.Ativo);

        return this;
    }

    public Usuario AtualizaUsuario(string username, string email, string nome, GrupoUsuario grupoUsuario)
    {
        Set(u => u.Username, username)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.UsernameObrigatorio);

        Set(u => u.GrupoUsuario, grupoUsuario)
            .ValidateWhen()
            .IsNull()
            .AddFailure(Erros.Usuario.GrupoUsuarioInvalido);

        Set(u => u.Nome, nome)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.NomeObrigatorio);

        Set(u => u.Email, email)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.EmailObrigatorio);

        return this;
    }

    public Usuario AtualizaSenhaUsuario(string password, string passwordHash)
    {
        Set(u => u.Password => password)
          .ValidateWhen()
          .IsNullOrEmpty()
          .AddFailure(Erros.Usuario.PasswordObrigatorio);

        Set(u => u.PasswordHash => passwordHash)
            .ValidateWhen()
            .IsNullOrEmpty()
            .AddFailure(Erros.Usuario.PasswordHashObrigatorio);

        return this;
    }

    public void RegistraUltimoAcesso()
    {
        this.UltimoAcesso = DateTime.Now;
    }
}
