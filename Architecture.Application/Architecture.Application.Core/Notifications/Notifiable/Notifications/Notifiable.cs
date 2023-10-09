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
    public bool HasNotifications()
    {
        return Notifications.HasNotifications;
    }
}