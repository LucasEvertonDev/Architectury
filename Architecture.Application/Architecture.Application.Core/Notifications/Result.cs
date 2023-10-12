using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;

namespace Architecture.Application.Core.Notifications;

public class Result
{
    public Result(NotificationContext Notification)
    {
        NotificationContext = Notification;
    }

    private NotificationContext NotificationContext { get; set; }

    public bool HasFailures() => NotificationContext.HasNotifications;

    public IReadOnlyCollection<NotificationModel> GetFailures => NotificationContext.Notifications;

    public Result Failure<T>(NotificationModel notification)
    {
        NotificationContext.AddNotification(notification);
        return this;
    }

    public Result Failure<T>(INotifiableModel notifiableModel)
    {
        NotificationContext.AddNotifications(notifiableModel.GetFailures());
        return this;
    }

    public NotificationContext GetContext() => NotificationContext;

    public dynamic Data { get; set; }
}
