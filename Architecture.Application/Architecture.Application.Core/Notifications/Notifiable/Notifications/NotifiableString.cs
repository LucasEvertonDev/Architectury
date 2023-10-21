using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterSet;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;
using System.Runtime.CompilerServices;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable<TEntity> : INotifiableModel
{
    protected AfterSet<AfterValidationWhenString> Set(Func<string, string> setFunc, [CallerArgumentExpression("setFunc")] dynamic argumentExpression = null)
    {
        this.SetValue(argumentExpression, setFunc(null));

        return new AfterSet<AfterValidationWhenString>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }
}