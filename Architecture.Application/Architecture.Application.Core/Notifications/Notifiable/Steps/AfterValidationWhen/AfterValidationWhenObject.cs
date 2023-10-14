using Architecture.Application.Core.Notifications.Notifiable.Steps.AddNotification;

namespace Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;

public class AfterValidationWhenObject : AfterValidationWhen, IAfterValidationWhen
{
    protected object _currentvalue { get; set; }

    public AfterValidationWhenObject(NotificationContext notificationContext, NotificationInfo notificationInfo) : base(notificationContext, notificationInfo)
    {
        _currentvalue = notificationInfo.PropInfo.Value;
    }

    public AddNotificationService<AfterValidationWhenObject> Is(bool ruleFor)
    {
        return new AddNotificationService<AfterValidationWhenObject>(_notificationContext, ruleFor, _notificationInfo);
    }

    public AddNotificationService<AfterValidationWhenObject> IsNull()
    {
        return new AddNotificationService<AfterValidationWhenObject>(_notificationContext, _notificationInfo.PropInfo.Value == null, _notificationInfo);
    }
}