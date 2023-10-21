using Architecture.Application.Domain.DbContexts.Domains;
using Architecture.Application.Domain.Models.Base;
using System.ComponentModel;

namespace Architecture.Application.Domain.Models.Usuarios;

public class UsuarioCriadoModel
{
    [DefaultValue("F97E565D-08AF-4281-FC11-C0206EAE06FA")]
    public string Id { get; set; }

    [DefaultValue("lcseverton")]
    public string Username { get; set; }

    [DefaultValue("F97E565D-08AF-4281-BC11-C0206EAE06FA")]
    public string GrupoUsuarioId { get; set; }
    [DefaultValue("Lucas Everton Santos de Oliveira")]
    public string Name { get; set; }
    [DefaultValue("lcseverton@gmail.com")]
    public string Email { get; set; }

    public UsuarioCriadoModel FromEntity(Usuario usuario)
    {
        return new UsuarioCriadoModel()
        {
            Email = usuario.Email,
            GrupoUsuarioId = usuario.GrupoUsuarioId.ToString(),
            Name = usuario.Nome,
            Username = usuario.Username,
            Id = usuario.Id.ToString()
        };
    }
}
