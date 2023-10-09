using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterSet;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;
using System.Linq.Expressions;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class DomainNotifiable<TEntity> : IDomainNotifiable
{
    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, DateTime>> memberLamda, DateTime value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, DateTime?>> memberLamda, DateTime? value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }
}