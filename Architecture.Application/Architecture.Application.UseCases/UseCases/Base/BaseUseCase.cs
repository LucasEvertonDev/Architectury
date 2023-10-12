using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Notifications.Context;
using Architecture.Application.Core.Notifications.Notifiable.Notifications;
using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Structure.UnitOfWork;
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
    public abstract Task<Result> ExecuteAsync(TParam param);

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
    protected async Task<Result> OnTransactionAsync(Func<Task<Result>> func)
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
        return Result;
    }

    protected async Task<Result> OnTransactionAsync(Func<Task> func)
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
        return Result;
    }
}


public abstract class BaseUseCase : Notifiable
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IIdentity _identity;
    public Result Result { get; private set; }

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
    public abstract Task<Result> ExecuteAsync();

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
    protected async Task<Result> OnTransactionAsync(Func<Task<Result>> func)
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

        return Result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    protected async Task<Result> OnTransactionAsync(Func<Task> func)
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

        return Result;
    }
}
