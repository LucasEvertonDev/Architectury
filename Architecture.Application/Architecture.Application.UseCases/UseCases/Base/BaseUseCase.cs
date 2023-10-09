using Architecture.Application.Core.Notifications.Context;
using Architecture.Application.Core.Notifications.Notifiable.Notifications;
using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Structure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Principal;

namespace Architecture.Application.UseCases.UseCases.Base;

public abstract class BaseUseCase<TParam, TRetorno> : Notifiable
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IIdentity _identity;

    public BaseUseCase(IServiceProvider serviceProvider)
    {
        _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
        _identity = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.User?.Identity;
        Notifications = serviceProvider.GetService<NotificationContext>();
    }

    public abstract Task<TRetorno> ExecuteAsync(TParam param);

    protected virtual Task OnSucess()
    {
        return Task.CompletedTask;
    }

    protected async virtual Task OnError(Exception exception)
    {
    }

    protected async Task<TRetorno> OnTransactionAsync(Func<Task<TRetorno>> func)
    {
        try
        {
            TRetorno retorno = await _unitOfWork.OpenConnectionAsync(func);
            return retorno;
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
