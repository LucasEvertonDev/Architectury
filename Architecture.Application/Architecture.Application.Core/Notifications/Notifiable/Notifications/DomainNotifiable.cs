using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using System.Linq.Expressions;
using System.Reflection;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class DomainNotifiable<TEntity> : IDomainNotifiable
{
    protected NotificationInfo NotificationInfo { get; set; }

    protected Result Result { get; set; }

    public DomainNotifiable()
    {
        NotificationContext = new NotificationContext();

        Result = new Result(NotificationContext);

        NotificationInfo = new NotificationInfo()
        {
            NotificationType = Enum.NotificationType.DomainNotification,
            Name = typeof(TEntity).Name,
            Namespace = typeof(TEntity).Namespace
        };
    }


    /// <summary>
    /// Notification Context
    /// </summary>
    protected NotificationContext NotificationContext { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<NotificationModel> GetNotifications()
    {
        return NotificationContext.Notifications.ToList();
    }

    /// <summary>
    /// Indica se o dominio é válido ou não
    /// </summary>
    /// <returns></returns>
    public bool HasNotifications()
    {
        return !GetNotifications().Any();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private void SetValue(dynamic lambda, dynamic value)
    {
        var memberSelectorExpression = lambda.Body as MemberExpression;
        if (memberSelectorExpression != null)
        {
            var property = memberSelectorExpression.Member as PropertyInfo;
            if (property != null)
            {
                property.SetValue(this, value, null);
                NotificationInfo.MemberName = !NotificationInfo.MainDomain ? string.Concat(NotificationInfo.Name, ".", property.Name) : property.Name;
            }
        }
        NotificationInfo.Value = value;
    }
}