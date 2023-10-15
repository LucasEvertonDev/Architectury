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

        var notificationInfo = new NotificationInfo(
            new PropInfo()
            {
                Value = null,
                MemberName = getName(exp)  
            },
            new EntityInfo()
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
        List<string> names = new List<string>();
        var memberSelectorExpression = lambda.Body as MemberExpression;
        if (memberSelectorExpression != null)
        {
            var property = memberSelectorExpression.Member as PropertyInfo;

            if (property == null)
            {
                throw new Exception("É preciso adicionar {get; set;} a sua prop");
            }

            names.Add(property.Name);

            dynamic expression = memberSelectorExpression;

            while (ContainsProperty(expression, "Expression"))
            {
                if (expression.Expression is MemberExpression)
                {
                    names.Add(((MemberExpression)expression.Expression).Type.Name);
                    expression = expression.Expression;
                }
                else if (expression.Expression is ParameterExpression)
                {
                    names.Add(((ParameterExpression)expression.Expression).Type.Name);
                    expression = expression.Expression;
                }
                else if (expression.Expression is MethodCallExpression)
                {
                    var argumento = expression.Expression.Arguments[0];

                    if (ContainsProperty(argumento, "Expression"))
                    { 
                        if (argumento.Expression is ConstantExpression)
                        {
                            var valor = ((ConstantExpression)argumento.Expression).Value;
                            names.Add($"[{valor.GetType().GetField("i").GetValue(valor)}]");
                        }
                    }
                    else if (argumento is ConstantExpression)
                    {
                        names.Add($"[{((ConstantExpression)argumento).Value}]");
                    }

                    if (expression.Expression.Object is MemberExpression)
                    {
                        names.Add(((MemberExpression)expression.Expression.Object).Member.Name);
                    }
                    expression = expression.Expression.Object;
                }
            }
        }

        names.Reverse(0, names.Count());

        return string.Join(".", names).Replace(".[", "[");
    }

    public Result Failure<T>(INotifiableModel notifiableModel)
    {
        NotificationContext.AddNotifications(notifiableModel.GetFailures());
        return this;
    }
     
    private bool ContainsProperty(object obj, string name) => obj.GetType().GetProperty(name) != null;

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
