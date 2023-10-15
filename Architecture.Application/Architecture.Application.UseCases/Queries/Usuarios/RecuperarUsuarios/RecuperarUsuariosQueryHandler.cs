﻿using Architecture.Application.Core.Notifications;
using Architecture.Application.Mediator.Commands;
using MediatR;
using Architecture.Application.Mediator.Queries.Pessoas;
using Architecture.Application.Domain.Models.Usuarios;

namespace Architecture.Application.Mediator.Queries.Usuarios.RecuperarUsuarios;

public class RecuperarUsuariosQueryHandler : BaseCommandHandler, IRequestHandler<RecuperarPessoasQuery, Result>
{
    public RecuperarUsuariosQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<Result> Handle(RecuperarPessoasQuery request, CancellationToken cancellationToken)
    {
        return await OnTransactionAsync(async () =>
        {
            var pagedResult = await unitOfWork.UsuarioRepository.ToListAsync(
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                predicate: u => string.IsNullOrEmpty(request.Nome) || u.Nome.Contains(request.Nome)
            );

            return Result.IncludeResult(new UsuariosRecuperadosModel().FromEntity(pagedResult));
        });
    }
}
