using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Notifications.Context;
using Architecture.Application.Domain.Plugins.FluentValidation;
using MediatR;

namespace Architecture.Application.Mediator.Pipelines;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse> where TResponse : Result
{
    private readonly IFluentService _fluentService;
    private readonly NotificationContext _notificationContext;
    private Result Result { get; set; }

    public ValidationBehaviour(IFluentService fluentService, 
        NotificationContext notificationContext)
    {
        _fluentService = fluentService;
        _notificationContext = notificationContext;
        Result = new Result(notificationContext);
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) 
    {
        var failures = await _fluentService.ValidateParameterAsync(request);

        if (failures.Any())
        {
            Result.Failure(failures.ToList());

            return (TResponse)Result;
        }

        return await next();
    }
}
