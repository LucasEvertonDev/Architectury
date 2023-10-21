using Architecture.Application.Core.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Architecture.Application.Mediator.Commands.Usuarios.ExcluirUsuario;

public class ExcluirUsuarioCommand : IRequest<Result>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public string Id { get; set; }
}
