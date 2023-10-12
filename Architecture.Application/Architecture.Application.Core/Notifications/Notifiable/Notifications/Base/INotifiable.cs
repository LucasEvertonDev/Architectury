namespace Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;

public interface INotifiable
{
    bool HasNotifications();
}


public interface INotifiableModel
{
    List<NotificationModel> GetFailures();
}