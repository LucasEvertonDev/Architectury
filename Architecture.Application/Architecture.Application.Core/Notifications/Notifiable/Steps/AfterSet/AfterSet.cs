namespace Architecture.Application.Core.Notifications.Notifiable.Steps.AfterSet;

public class AfterSet<TNext>
{
    private readonly NotificationContext _notificationContext;
    private readonly NotificationInfo _notificationInfo;

    public AfterSet(NotificationContext notificationContext, NotificationInfo notificationInfo)
    {
        _notificationContext = notificationContext;
        _notificationInfo = notificationInfo;
    }

    public TNext When()
    {
        return (TNext)Activator.CreateInstance(typeof(TNext), _notificationContext, _notificationInfo);
    }
}
