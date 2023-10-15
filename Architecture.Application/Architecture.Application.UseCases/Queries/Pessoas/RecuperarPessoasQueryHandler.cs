using Architecture.Application.Core.Notifications;
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
        return await OnTransactionAsync(async () =>
        {
            var aux = await unitOfWork.PessoasRepository.GetPessoasQuery();

            return Result.IncludeResult(await unitOfWork.PessoasRepository.ToListAsync());
        });
    }
}
