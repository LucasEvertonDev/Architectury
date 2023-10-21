using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Models.Base;
using MediatR;
using System.ComponentModel;

namespace Architecture.Application.Mediator.Commands.Usuarios.CriarUsuario;

public class CriarUsuarioCommand : IRequest<Result>, IValidationAsync
{
    [DefaultValue("lcseverton")]
    public string Username { get; set; }
    [DefaultValue("123456")]
    public string Password { get; set; }
    [DefaultValue("F97E565D-08AF-4281-BC11-C0206EAE06FA")]
    public string GrupoUsuarioId { get; set; }
    [DefaultValue("Lucas Everton Santos de Oliveira")]
    public string Name { get; set; }
    [DefaultValue("lcseverton@gmail.com")]
    public string Email { get; set; }
}
