using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterSet;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;
using System.Linq.Expressions;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class DomainNotifiable<TEntity> : IDomainNotifiable
{
    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, short>> memberLamda, short value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, short?>> memberLamda, short? value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, int>> memberLamda, int value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, int?>> memberLamda, int? value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, long>> memberLamda, long value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, long?>> memberLamda, long? value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }
}

public partial record RecordNotifiable<TEntity> : IRecordNotifiable
{
    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, short>> memberLamda, short value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, short?>> memberLamda, short? value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, int>> memberLamda, int value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, int?>> memberLamda, int? value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, long>> memberLamda, long value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }

    protected AfterSet<AfterValidationWhenObject> Set(Expression<Func<TEntity, long?>> memberLamda, long? value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenObject>(NotificationContext, NotificationInfo);
    }
}