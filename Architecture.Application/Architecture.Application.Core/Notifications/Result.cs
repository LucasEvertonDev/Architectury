using Architecture.Application.Core.Notifications.Enum;
using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using System.Linq.Expressions;
using System.Reflection;

namespace Architecture.Application.Core.Notifications;

public class Result
{
    public Result(NotificationContext Notification)
    {
        NotificationContext = Notification;
    }

    private NotificationContext NotificationContext { get; set; }

    public bool HasFailures() => NotificationContext.HasNotifications;

    public IReadOnlyCollection<NotificationModel> GetFailures => NotificationContext.Notifications;

    public Result Failure<T>(FailureModel failure) where T : INotifiable
    {
        var notificationType = NotificationType.BusinessNotification;
        if (typeof(T).GetInterfaces().ToList().Exists(x => x.Name == nameof(INotifiableModel)))
        {
            notificationType = NotificationType.DomainNotification;
        }

        var notificationInfo = new NotificationInfo(new PropInfo(), new EntityInfo()
        {
            NotificationType = notificationType,
            Name = typeof(T).Name,
            Namespace = typeof(T).Namespace
        });

        NotificationContext.AddNotification(new NotificationModel(failure, notificationInfo));

        return this;
    }

    public Result Failure<T>(Expression<Func<T, dynamic>> exp, FailureModel failure) where T : INotifiableModel
    {
        var notificationType = NotificationType.BusinessNotification;
        if (typeof(T).GetInterfaces().ToList().Exists(x => x.Name == nameof(INotifiableModel)))
        {
            notificationType = NotificationType.DomainNotification;
        }

        var notificationInfo = new NotificationInfo(new PropInfo()
        {
            Value = null,
            MemberName = getName(exp)  
        }, new EntityInfo()
        {
            NotificationType = notificationType,
            Name = typeof(T).Name,
            Namespace = typeof(T).Namespace
        });

        NotificationContext.AddNotification(new NotificationModel(failure, notificationInfo));

        return this;
    }

    private string getName(dynamic lambda)
    {
        var memberSelectorExpression = lambda.Body as MemberExpression;
        if (memberSelectorExpression != null)
        {
            //memberSelectorExpression.Expression 

            var property = memberSelectorExpression.Member as PropertyInfo;
            if (property != null)
            {
                //CurrentProp.MemberName = value is INotifiableModel ? EntityInfo.Name : string.Concat(EntityInfo.Name, ".", property.Name);
            }
            else
            {
                throw new Exception("É preciso adicionar {get; set;} a sua prop");
            }
        }

        return "";
    }

    public Result Failure<T>(INotifiableModel notifiableModel)
    {
        NotificationContext.AddNotifications(notifiableModel.GetFailures());
        return this;
    }

    public NotificationContext GetContext() => NotificationContext;

    public Result IncludeResult(dynamic value)
    {
        Data = value;
        return this;
    }
    public T GetValue<T>()
    {
        return (T)Data;
    }
    private dynamic Data { get; set; }
}
