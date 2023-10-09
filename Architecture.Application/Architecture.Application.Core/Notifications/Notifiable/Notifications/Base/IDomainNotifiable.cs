namespace Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;

public interface IDomainNotifiable : INotifiable
{
    List<NotificationModel> GetNotifications();

    bool HasNotifications();
}