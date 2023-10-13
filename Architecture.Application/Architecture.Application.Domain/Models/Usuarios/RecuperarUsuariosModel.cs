using Architecture.Application.Core.Structure.Models;
using Architecture.Application.Domain.DbContexts.Domains;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Architecture.Application.Domain.Models.Usuarios;

public class RecuperarUsuariosDto
{
    public RecuperarUsuariosDto() { }

    [DefaultValue("1")]
    [FromRoute(Name = "pagenumber")]
    public int PageNumber { get; set; }

    [DefaultValue("10")]
    [FromRoute(Name = "pagesize")]
    public int PageSize { get; set; }

    [FromQuery(Name = "name")]
    public string Nome { get; set; }
}

public class UsuariosRecuperadosModel
{
    [DefaultValue("F97E565D-08AF-4281-FC11-C0206EAE06FA")]
    public string Id { get; set; }

    [DefaultValue("lcseverton")]
    public string Username { get; set; }

    [DefaultValue("F97E565D-08AF-4281-BC11-C0206EAE06FA")]
    public string GrupoUsuarioId { get; set; }
    [DefaultValue("Lucas Everton Santos de Oliveira")]
    public string Nome { get; set; }
    [DefaultValue("lcseverton@gmail.com")]
    public string Email { get; set; }

    public PagedResult<UsuariosRecuperadosModel> FromEntity(PagedResult<Usuario> paged)
    {
        var usuarios = new List<UsuariosRecuperadosModel>();

        foreach(var usuario in paged.Items)
        {
            usuarios.Add(new UsuariosRecuperadosModel()
            {
                Email = usuario.Email,
                GrupoUsuarioId = usuario.GrupoUsuarioId.ToString(),
                Nome = usuario.Nome,
                Username = usuario.Username,
                Id = usuario.Id.ToString()
            });
        }

        return new PagedResult<UsuariosRecuperadosModel>(usuarios, pageNumber: paged.PageNumber, pageSize: paged.PageSize, totalElements: paged.TotalElements);
    }
}
