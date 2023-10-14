using Architecture.Application.Core.Notifications.Enum;

namespace Architecture.Application.Core.Notifications;

public class NotificationInfo
{
    public NotificationInfo(PropInfo propInfo, EntityInfo entityInfo)
    {
        PropInfo = propInfo;
        EntityInfo = entityInfo;
    }

    public PropInfo PropInfo { get; set; }

    public EntityInfo EntityInfo { get; set; }
}


public class PropInfo
{
    public dynamic Value { get; set; }

    public string MemberName { get; set; }

    public void SetMemberNamePrefix(string prefix)
    {
        MemberName = string.IsNullOrEmpty(prefix) ? MemberName : string.Concat(prefix, ".", MemberName);
    }
}

public class EntityInfo
{
    public string Name { get; set; }

    public string Namespace { get; set; }

    public NotificationType NotificationType { get; set; } = NotificationType.BusinessNotification;
}