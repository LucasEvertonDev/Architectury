using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Domain.Models.Usuarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Architecture.Application.Mediator.Commands.Usuarios.AtualizarUsuario;

public class AtualizarUsuarioCommand : IRequest<Result>, IValidationAsync
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual string Id { get; set; }

    [FromBody]
    public AtualizarUsuarioModel Body { get; set; }
}
