﻿using Architecture.Application.Domain.DbContexts.Domains;
using System.ComponentModel;

namespace Architecture.Application.Domain.Models.Usuarios;

public class AtualizarUsuarioModel
{
    [DefaultValue("lcseverton")]
    public string Username { get; set; }

    [DefaultValue("F97E565D-08AF-4281-BC11-C0206EAE06FA")]
    public string GrupoUsuarioId { get; set; }
    [DefaultValue("Lucas Everton Santos de Oliveira")]
    public string Nome { get; set; }
    [DefaultValue("lcseverton@gmail.com")]
    public string Email { get; set; }
}

public class UsuarioAtualizadoModel
{
    [DefaultValue("F97E565D-08AF-4281-FC11-C0206EAE06FA")]
    public string Id { get; set; }
    [DefaultValue("lcseverton")]
    public string Username { get; set; }

    [DefaultValue("F97E565D-08AF-4281-BC11-C0206EAE06FA")]
    public Guid GrupoUsuarioId { get; set; }
    [DefaultValue("Lucas Everton Santos de Oliveira")]
    public string Nome { get; set; }
    [DefaultValue("lcseverton@gmail.com")]
    public string Email { get; set; }

    public UsuarioAtualizadoModel FromEntity(Usuario usuario)
    {
        return new UsuarioAtualizadoModel()
        {
            Email = usuario.Email,
            GrupoUsuarioId = usuario.GrupoUsuarioId,
            Nome = usuario.Nome,
            Username = usuario.Username,
            Id = usuario.Id.ToString()
        };
    }
}
