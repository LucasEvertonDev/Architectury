using Architecture.Application.Core.Notifications;
using Architecture.Application.Domain.Constants;
using MediatR;

namespace Architecture.Application.Mediator.Commands.Usuarios.ExcluirUsuario;

public class ExcluirUsuarioCommandHandler : BaseCommandHandler, IRequestHandler<ExcluirUsuarioCommand, Result>
{
    public ExcluirUsuarioCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<Result> Handle(ExcluirUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await unitOfWork.UsuarioRepository.FirstOrDefaultAsync(u => u.Id.ToString() == request.Id);

        if (usuario == null)
        {
            return Result.Failure<ExcluirUsuarioCommandHandler>(Erros.Business.UsuarioInexistente);
        }

        await unitOfWork.UsuarioRepository.DeleteLogicAsync(usuario);

        return Result;
    }
}
