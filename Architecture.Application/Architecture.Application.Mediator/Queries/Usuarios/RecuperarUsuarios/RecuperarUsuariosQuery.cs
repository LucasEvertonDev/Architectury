using Architecture.Application.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Architecture.Application.Mediator.Queries.Usuarios.RecuperarUsuarios;

public class RecuperarUsuariosQuery : IRequest<Result>
{

    [DefaultValue("1")]
    [FromRoute(Name = "pagenumber")]
    public int PageNumber { get; set; }

    [DefaultValue("10")]
    [FromRoute(Name = "pagesize")]
    public int PageSize { get; set; }

    [FromQuery(Name = "name")]
    public string Nome { get; set; }
}
