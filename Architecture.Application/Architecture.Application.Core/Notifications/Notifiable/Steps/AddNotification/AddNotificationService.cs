using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;

namespace Architecture.Application.Core.Notifications.Notifiable.Steps.AddNotification;

public class AddNotificationService<TOut> where TOut : IAfterValidationWhen
{
    private readonly NotificationContext _notificationContext;
    private readonly bool _includeNotification;
    private readonly dynamic _value;
    private NotificationInfo _notificationInfo { get; set; }

    public AddNotificationService(NotificationContext notificationContext, bool includeNotification, NotificationInfo notificationInfo)
    {
        _notificationContext = notificationContext;
        _includeNotification = includeNotification;
        _notificationInfo = notificationInfo;
        _value = notificationInfo.Value;
    }

    public TOut AddFailure(NotificationModel notification)
    {
        if (_includeNotification)
        {
            notification.SetNotificationInfo(_notificationInfo);
            _notificationContext.AddNotification(notification);
        }

        return (TOut)Activator.CreateInstance(typeof(TOut), _notificationContext, _notificationInfo);
    }
}