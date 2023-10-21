using Architecture.Application.Core.Notifications.Context;
using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Notifications.Notifiable.Notifications;
using Architecture.Application.Domain.DbContexts.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Principal;
using MediatR;
using Architecture.Application.Domain.Models.Base;

namespace Architecture.Application.Mediator.Commands;

public class BaseCommandHandler : Notifiable
{
    private IUnitWorkTransaction _unitOfWorkTransaction;
    private readonly IServiceProvider _serviceProvider;
    protected readonly IIdentity _identity;
    public Result Result { get; private set; }

    public BaseCommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _identity = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.User?.Identity;
        Notifications = serviceProvider.GetService<NotificationContext>();
        Result = new Result(Notifications);
    }

    /// <summary>
    /// Tem seu bind no momento da invocação do metodo on transaction
    /// </summary>
    public IUnitOfWork unitOfWork => _unitOfWorkTransaction;


    protected virtual Task OnSucess()
    {
        return Task.CompletedTask;
    }

    protected virtual Task OnError(Exception exception)
    {
        return Task.CompletedTask;
    }

    protected async Task<Result> OnTransactionAsync(Func<Task<Result>> func)
    {
        try
        {
            _unitOfWorkTransaction = _serviceProvider.GetService<IUnitWorkTransaction>();

            await _unitOfWorkTransaction.OpenConnectionAsync(func);
        }
        catch (Exception exception)
        {
            await OnError(exception);
            throw;
        }
        finally
        {
            await OnSucess();
        }
        return Result;
    }

    protected async Task<Result> OnTransactionAsync(Func<Task> func)
    {
        try
        {
            _unitOfWorkTransaction = _serviceProvider.GetService<IUnitWorkTransaction>();

            await _unitOfWorkTransaction.OpenConnectionAsync(func);
        }
        catch (Exception exception)
        {
            await OnError(exception);
            throw;
        }
        finally
        {
            await OnSucess();
        }
        return Result;
    }
}