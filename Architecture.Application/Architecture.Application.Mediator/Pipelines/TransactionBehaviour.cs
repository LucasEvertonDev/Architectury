using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Notifications.Context;
using Architecture.Application.Domain.DbContexts.UnitOfWork;
using Architecture.Application.Domain.Plugins.FluentValidation;
using MediatR;

namespace Architecture.Application.Mediator.Pipelines;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : Result
{
    private readonly IUnitWorkTransaction _unitWorkTransaction;

    public TransactionBehaviour(IUnitWorkTransaction unitWorkTransaction)
    {
        _unitWorkTransaction = unitWorkTransaction;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await _unitWorkTransaction.OnTransactionAsync(async () =>
            {
                return await next();
            });
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            throw;
        }
    }
}
