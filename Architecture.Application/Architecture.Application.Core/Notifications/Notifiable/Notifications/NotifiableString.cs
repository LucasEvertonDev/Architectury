using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterSet;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;
using System.Linq.Expressions;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable<TEntity> : INotifiableModel
{
    protected AfterSet<AfterValidationWhenString> Set(Expression<Func<TEntity, string>> memberLamda, string value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenString>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }

    protected AfterSet<AfterValidationWhenString> Set(Expression<Func<string>> expression, string value)
    {
        
        this.SetValue2(expression, value);

        return new AfterSet<AfterValidationWhenString>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }

    protected AfterSet<AfterValidationWhenString> Set(object prop, string value)
    {

        ////this.SetValue2(expression, value);

        return new AfterSet<AfterValidationWhenString>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }

    protected AfterSet<AfterValidationWhenString> Set2(Func<object, string> expression, string value)
    {

        ////this.SetValue2(expression, value);

        return new AfterSet<AfterValidationWhenString>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }
}