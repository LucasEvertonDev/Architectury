using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterSet;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;
using Architecture.Application.Core.Structure.Extensions;
using System.Linq.Expressions;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable<TEntity> : INotifiableModel
{
    /// <summary>
    ///  Quando record as notificaçoes são integradas de forma interna 
    /// </summary>
    /// <param name="memberLamda"></param>
    /// <param name="value"></param>
    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, INotifiableModel>> memberLamda, INotifiableModel value)
    {
        this.SetValue(memberLamda, value);

        for (var i = 0; i < value.GetFailures().Count(); i++)
        {
            var failure = value.GetFailures()[i];

            failure.SetMemberNamePrefix(NotificationInfo.MemberName);

            this.Result.GetContext().AddNotification(failure);
        }

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), NotificationInfo);
    }

    /// <summary>
    ///  Quando record as notificaçoes são integradas de forma interna 
    /// </summary>
    /// <param name="memberLamda"></param>
    /// <param name="value"></param>
    protected AfterSet<AfterValidationWhenObject> Set<T>(Expression<Func<TEntity, List<T>>> memberLamda, List<T> value) where T : INotifiableModel
    {
        this.SetValue(memberLamda, value);

        var failures = value.GetNotifications(NotificationInfo.MemberName);

        for (var i = 0; i < failures.Count; i++)
        {
            var failure = failures[i];

            this.Result.GetContext().AddNotification(failure);
        }

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), NotificationInfo);
    }
}