using Architecture.Application.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Architecture.Application.Mediator.Queries.Pessoas;

public class RecuperarPessoasQuery : IRequest<Result>
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
