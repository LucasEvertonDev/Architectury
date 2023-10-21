namespace Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;

public interface INotifiable
{
    bool HasFailure();
}


public interface INotifiableModel
{
    List<NotificationModel> GetFailures();
}