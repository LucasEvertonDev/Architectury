using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Base;
using Architecture.Application.Domain.Models.Usuarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Architecture.Application.Mediator.Commands.Usuarios.AtualizarSenha;

public class AtualizarSenhaCommand : IRequest<Result>, IValidationAsync
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual string Id { get; set; }

    [FromBody]
    public AtualizarSenhaUsuarioModel Body { get; set; }
}
