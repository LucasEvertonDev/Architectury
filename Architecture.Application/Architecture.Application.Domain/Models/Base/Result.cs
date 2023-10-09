namespace Architecture.Application.Domain.Models.Base;

public class Result
{
    public Result(NotificationContext Notification)
    {
        NotificationContext = Notification;
    }

    private NotificationContext NotificationContext { get; set; }

    public bool IsValid() => !NotificationContext.HasNotifications;

    public IReadOnlyCollection<NotificationModel> GetNotifications => NotificationContext.Notifications;

    public void Failure<T>(NotificationModel notification)
    {
        NotificationContext.AddNotification(notification);
    }

    public dynamic Data { get; set; }
}
