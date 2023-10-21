using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterSet;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable<TEntity> : INotifiableModel
{
    protected AfterSet<AfterValidationWhenObject> Set(Func<short, short> setFunc, [CallerArgumentExpression("setFunc")] dynamic argumentExpression = null)
    {
        this.SetValue(argumentExpression, setFunc(0));

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }

    protected AfterSet<AfterValidationWhenObject> Set(Func<short?, short?> setFunc, [CallerArgumentExpression("setFunc")] dynamic argumentExpression = null)
    {
        this.SetValue(argumentExpression, setFunc(0));

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }

    protected AfterSet<AfterValidationWhenObject> Set(Func<int, int> setFunc, [CallerArgumentExpression("setFunc")] dynamic argumentExpression = null)
    {
        this.SetValue(argumentExpression, setFunc(0));

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }

    protected AfterSet<AfterValidationWhenObject> Set(Func<int?, int?> setFunc, [CallerArgumentExpression("setFunc")] dynamic argumentExpression = null)
    {
        this.SetValue(argumentExpression, setFunc(0));

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }

    protected AfterSet<AfterValidationWhenObject> Set(Func<long, long> setFunc, [CallerArgumentExpression("setFunc")] dynamic argumentExpression = null)
    {
        this.SetValue(argumentExpression, setFunc(0));

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }

    protected AfterSet<AfterValidationWhenObject> Set(Func<long?, long?> setFunc, [CallerArgumentExpression("setFunc")] dynamic argumentExpression = null)
    {
        this.SetValue(argumentExpression, setFunc(0));

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }
}
