using Architecture.Application.Core.Notifications.Enum;

namespace Architecture.Application.Core.Notifications;

public class NotificationInfo
{
    public bool MainDomain  { get; set; }
    public string Name { get; set; }
    public dynamic Value { get; set; }
    public string Namespace { get; set; }
    public string MemberName { get; set; }

    public string Prefix { get; set; } 
    public NotificationType NotificationType { get; set; } = NotificationType.BusinessNotification;
}
