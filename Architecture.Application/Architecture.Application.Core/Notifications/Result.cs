using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Services;
using System.Linq.Expressions;

namespace Architecture.Application.Core.Notifications;

public class Result
{
    public Result(NotificationContext Notification)
    {
        NotificationContext = Notification;
        _resultService = new ResultService(Notification);
    }

    private ResultService _resultService { get; set; }

    private NotificationContext NotificationContext { get; set; }

    public bool HasFailures() => NotificationContext.HasNotifications;

    public IReadOnlyCollection<NotificationModel> GetFailures => NotificationContext.Notifications;

    public Result Failure<T>(FailureModel failure) where T : INotifiable
    {
        _resultService.Failure<T>(failure);
        return this;
    }

    public Result Failure<T>(Expression<Func<T, dynamic>> exp, FailureModel failure) where T : INotifiableModel
    {
        _resultService.Failure<T>(exp, failure);
        return this;
    }

    public Result Failure<T>(INotifiableModel notifiableModel)
    {
        NotificationContext.AddNotifications(notifiableModel.GetFailures());
        return this;
    }

    public Result Failure(List<NotificationModel> failures)
    {
        NotificationContext.AddNotifications(failures);
        return this;
    }

    public NotificationContext GetContext() => NotificationContext;

    public T GetContent<T>()
    {
        return (T)Content;
    }

    public Result SetContent(dynamic content)
    {
        Content = content;
        return this;
    }

    private dynamic Content { get; set; }
}
