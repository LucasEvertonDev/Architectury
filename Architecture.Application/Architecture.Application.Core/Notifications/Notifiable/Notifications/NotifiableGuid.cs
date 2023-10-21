using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterSet;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;
using System.Runtime.CompilerServices;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable<TEntity> : INotifiableModel
{
    protected AfterSet<AfterValidationWhenObject> Set(Func<Guid, Guid> setFunc, [CallerArgumentExpression("setFunc")] dynamic argumentExpression = null)
    {
        this.SetValue(argumentExpression, setFunc(new Guid()));

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }
}
