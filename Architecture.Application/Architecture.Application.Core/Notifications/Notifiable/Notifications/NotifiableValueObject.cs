using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterSet;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;
using Architecture.Application.Core.Structure.Extensions;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable<TEntity> : INotifiableModel
{

    protected AfterSet<AfterValidationWhenObject> Set(Func<INotifiableModel, INotifiableModel> setFunc, [CallerArgumentExpression("setFunc")] dynamic argumentExpression = null)
    {
        var value = setFunc(null);

        this.SetValue(argumentExpression, value);

        for (var i = 0; i < value?.GetFailures()?.Count(); i++)
        {
            var failure = value.GetFailures()[i];

            var notification = failure.Clone();

            notification.NotificationInfo.PropInfo.SetMemberNamePrefix(CurrentProp.MemberName);

            this.Result.GetContext().AddNotification(notification);
        }

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }


    protected AfterSet<AfterValidationWhenObject> Set<T>(Func<List<T>, List<T>> setFunc, [CallerArgumentExpression("setFunc")] dynamic argumentExpression = null) where T : INotifiableModel
    {
        List<T> value = setFunc(Activator.CreateInstance<List<T>>());

        this.SetValue(argumentExpression, value);

        var failures = value.GetNotifications(CurrentProp.MemberName);

        for (var i = 0; i < failures?.Count; i++)
        {
            var failure = failures[i];

            this.Result.GetContext().AddNotification(failure);
        }

        return new AfterSet<AfterValidationWhenObject>(Result.GetContext(), new NotificationInfo(CurrentProp, EntityInfo));
    }
}