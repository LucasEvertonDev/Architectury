using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable : INotifiable
{
    /// <summary>
    /// Notification Context
    /// </summary>
    protected NotificationContext Notifications { get; set; }

    protected NotificationInfo NotificationInfo { get; set; }

    /// <summary>
    /// Set Notification Context
    /// </summary>
    /// <param name="context"></param>
    public void SetNotificationContext(NotificationContext context) => Notifications = context;

    /// <summary>
    /// Indica se o dominio é válido ou não
    /// </summary>
    /// <returns></returns>
    public bool HasError()
    {
        return Notifications.HasNotifications;
    }

    /// <summary>
    /// Instancia classe para trabalhar com notificationPattern
    /// </summary>
    /// <typeparam name="TNotifiable"></typeparam>
    /// <returns></returns>
    protected TNotifiable Notify<TNotifiable>() where TNotifiable : INotifiable
    {
        var entity = Activator.CreateInstance<TNotifiable>();
        entity.SetNotificationContext(Notifications);
        return entity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void SetAggregateRoot(bool value)
    {
        NotificationInfo.MainDomain = false;
    }
}