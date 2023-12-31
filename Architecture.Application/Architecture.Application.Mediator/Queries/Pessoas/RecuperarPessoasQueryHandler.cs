﻿using Architecture.Application.Core.Notifications;
using Architecture.Application.Mediator.Commands;
using MediatR;

namespace Architecture.Application.Mediator.Queries.Pessoas;

public class RecuperarPessoasQueryHandler : BaseCommandHandler, IRequestHandler<RecuperarPessoasQuery, Result>
{
    public RecuperarPessoasQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public  async Task<Result> Handle(RecuperarPessoasQuery request, CancellationToken cancellationToken)
    {
        var aux = await unitOfWork.PessoasRepository.GetPessoasQuery();

        return Result.SetContent(await unitOfWork.PessoasRepository.ToListAsync());
    }
}
