using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using System.Reflection;

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

    public Result Failure<T>(NotificationModel notification) where T : class
    {
        var notificationType = Enum.NotificationType.BusinessNotification;
        if (typeof(T).GetInterfaces().ToList().Exists(x => x.Name == nameof(INotifiableModel)))
        {
            notificationType = Enum.NotificationType.DomainNotification;
        }

        var notificationInfo = new NotificationInfo()
        {
            NotificationType = notificationType,
            Name = typeof(T).Name,
            Namespace = typeof(T).Namespace
        };

        notification.SetNotificationInfo(notificationInfo);

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
