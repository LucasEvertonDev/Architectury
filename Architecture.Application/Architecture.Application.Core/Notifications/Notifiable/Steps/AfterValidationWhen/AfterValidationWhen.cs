using Architecture.Application.Core.Notifications.Notifiable.Steps.AddNotification;

namespace Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;

public class AfterValidationWhen : IAfterValidationWhen
{
    protected NotificationInfo _notificationInfo { get; set; }
    protected readonly NotificationContext _notificationContext;
    protected readonly dynamic _value;

    public AfterValidationWhen(NotificationContext notificationContext, NotificationInfo notificationInfo)
    {
        _notificationContext = notificationContext;
        _notificationInfo = notificationInfo;
        _value = notificationInfo.Value;
    }
}
