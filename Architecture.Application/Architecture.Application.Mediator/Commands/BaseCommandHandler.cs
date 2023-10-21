using Architecture.Application.Core.Notifications.Context;
using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Notifications.Notifiable.Notifications;
using Architecture.Application.Domain.DbContexts.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Principal;

namespace Architecture.Application.Mediator.Commands;

public class BaseCommandHandler : Notifiable
{
    private IUnitWorkTransaction _unitOfWorkTransaction;
    protected readonly IIdentity _identity;
    public Result Result { get; private set; }

    public BaseCommandHandler(IServiceProvider serviceProvider)
    {
        _identity = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.User?.Identity;
        _unitOfWorkTransaction = serviceProvider.GetService<IUnitWorkTransaction>();
        Notifications = serviceProvider.GetService<NotificationContext>();
        Result = new Result(Notifications);
    }

    /// <summary>
    /// Tem seu bind no momento da invocação do metodo on transaction
    /// </summary>
    public IUnitOfWork unitOfWork => _unitOfWorkTransaction;
}