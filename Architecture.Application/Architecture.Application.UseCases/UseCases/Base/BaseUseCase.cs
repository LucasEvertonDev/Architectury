using Architecture.Application.Core.Notifications.Context;
using Architecture.Application.Core.Notifications.Notifiable.Notifications;
using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Structure.UnitOfWork;
using Architecture.Application.Domain.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Principal;

namespace Architecture.Application.UseCases.UseCases.Base;

public abstract class BaseUseCase<TParam> : Notifiable
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IIdentity _identity;
    public Result Result { get; private set; }

    public BaseUseCase(IServiceProvider serviceProvider)
    {
        _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
        _identity = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.User?.Identity;
        Notifications = serviceProvider.GetService<NotificationContext>();
        Result = new Result(Notifications);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public abstract Task ExecuteAsync(TParam param);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual Task OnSucess()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    protected virtual Task OnError(Exception exception)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    protected async Task OnTransactionAsync(Func<Task> func)
    {
        try
        {
            await _unitOfWork.OpenConnectionAsync(func);
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
    }

    /// <summary>
    /// Instancia classe para trabalhar com notificationPattern
    /// </summary>
    /// <typeparam name="TNotifiable"></typeparam>
    /// <returns></returns>
    protected TNotifiable Notifiable<TNotifiable>() where TNotifiable : INotifiable
    {
        var entity = Activator.CreateInstance<TNotifiable>();
        entity.SetNotificationContext(Notifications);
        entity.SetAggregateRoot(true);
        return entity;
    }
}


public abstract class BaseUseCase : Notifiable
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IIdentity _identity;

    public BaseUseCase(IServiceProvider serviceProvider)
    {
        _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
        _identity = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.User?.Identity;
        Notifications = serviceProvider.GetService<NotificationContext>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public abstract Task ExecuteAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual Task OnSucess()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    protected virtual Task OnError(Exception exception)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    protected async Task OnTransactionAsync(Func<Task> func)
    {
        try
        {
            await _unitOfWork.OpenConnectionAsync(func);
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
    }

    /// <summary>
    /// Instancia classe para trabalhar com notificationPattern
    /// </summary>
    /// <typeparam name="TNotifiable"></typeparam>
    /// <returns></returns>
    protected TNotifiable Notifiable<TNotifiable>() where TNotifiable : INotifiable
    {
        var entity = Activator.CreateInstance<TNotifiable>();
        entity.SetNotificationContext(Notifications);
        entity.SetAggregateRoot(true);
        return entity;
    }
}
