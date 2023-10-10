using Architecture.Application.Core.Notifications.Enum;

namespace Architecture.Application.Core.Notifications;

public record NotificationModel
{
    public NotificationModel(string key, string message)
    {
        this.key = key;
        this.message = message;
    }

    public void SetNotificationInfo(NotificationInfo notificationInfo)
    {
        this.context = notificationInfo.Namespace;
        this.member = notificationInfo.MemberName;
        this.notificationType = notificationInfo.NotificationType;
    }

    public void SetMemberNamePrefix(string prefix)
    {
        this.member = string.IsNullOrEmpty(prefix) ? this.member : string.Concat(prefix, ".", this.member);
    }

    public string key { get; private set; }
    public string message { get; private set; }
    public string context { get; private set; }
    public string member { get; private set; }
    public NotificationType notificationType { get; private set; } = NotificationType.BusinessNotification; 
} 

