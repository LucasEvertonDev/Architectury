using Architecture.Application.Core.Notifications.Enum;
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

    public Result Failure<T>(FailureModel failure) where T : class
    {
        var notificationType = NotificationType.BusinessNotification;
        if (typeof(T).GetInterfaces().ToList().Exists(x => x.Name == nameof(INotifiableModel)))
        {
            notificationType = NotificationType.DomainNotification;
        }

        var notificationInfo = new NotificationInfo(new PropInfo(), new EntityInfo()
        {
            NotificationType = notificationType,
            Name = typeof(T).Name,
            Namespace = typeof(T).Namespace
        });

        NotificationContext.AddNotification(new NotificationModel(failure, notificationInfo));

        return this;
    }

    public Result Failure<T>(INotifiableModel notifiableModel)
    {
        NotificationContext.AddNotifications(notifiableModel.GetFailures());
        return this;
    }

    public NotificationContext GetContext() => NotificationContext;

    public Result IncludeResult(dynamic value)
    {
        Data = value;
        return this;
    }
    public T GetValue<T>()
    {
        return (T)Data;
    }
    private dynamic Data { get; set; }
}
