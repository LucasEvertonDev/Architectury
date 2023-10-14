using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterSet;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;
using System.Linq.Expressions;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable<TEntity> : INotifiableModel
{
    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, Decimal>> memberLamda, Decimal value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, Decimal?>> memberLamda, Decimal? value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }
}