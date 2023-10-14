using Architecture.Application.Core.Notifications;
using Architecture.Application.Core.Notifications.Context;
using Architecture.Application.Core.Notifications.Notifiable.Notifications;
using Architecture.Application.Domain.DbContexts.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Principal;

namespace Architecture.Application.UseCases.UseCases.Base;

public abstract class BaseUseCase<TParam> : Notifiable
{
    private IUnitWorkTransaction _unitOfWorkTransaction;
    protected readonly IIdentity _identity;
    private readonly IServiceProvider _serviceProvider;

    public Result Result { get; private set; }

    public BaseUseCase(IServiceProvider serviceProvider)
    {
        _identity = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.User?.Identity;
        _serviceProvider = serviceProvider;
        Notifications = serviceProvider.GetService<NotificationContext>();
        Result = new Result(Notifications);
    }

    public IUnitOfWork UnitOfWork => _unitOfWorkTransaction;

    public abstract Task<Result> ExecuteAsync(TParam param);

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

public abstract class BaseUseCase : Notifiable
{
    protected IUnitWorkTransaction _unitOfWorkTransaction;
    private readonly IServiceProvider _serviceProvider;
    protected readonly IIdentity _identity;
    public Result Result { get; private set; }

    public BaseUseCase(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _identity = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.User?.Identity;
        Notifications = serviceProvider.GetService<NotificationContext>();
        Result = new Result(Notifications);
    }

    protected IUnitOfWork UnitOfWork => _unitOfWorkTransaction;


    public abstract Task<Result> ExecuteAsync();

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
