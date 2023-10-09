namespace Architecture.Application.Core.Notifications;

public class Result
{
    public Result(NotificationContext Notification)
    {
        NotificationContext = Notification;
    }

    private NotificationContext NotificationContext { get; set; }

    public bool HasNotifications() => NotificationContext.HasNotifications;

    public IReadOnlyCollection<NotificationModel> GetNotifications => NotificationContext.Notifications;

    public void Failure<T>(NotificationModel notification)
    {
        NotificationContext.AddNotification(notification);
    }

    public void Failure<T>(List<NotificationModel> notifications)
    {
        NotificationContext.AddNotifications(notifications);
    }

    public dynamic Data { get; set; }
}
