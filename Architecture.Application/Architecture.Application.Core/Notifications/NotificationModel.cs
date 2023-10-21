namespace Architecture.Application.Core.Notifications;

public record NotificationModel
{
    public NotificationModel(FailureModel failure, NotificationInfo notificationInfo)
    {
        Error = failure;
        NotificationInfo = notificationInfo;
    }

    public FailureModel Error { get; set; }
    public NotificationInfo NotificationInfo { get; set; }
}

public record FailureModel(string code, string message)
{

}

