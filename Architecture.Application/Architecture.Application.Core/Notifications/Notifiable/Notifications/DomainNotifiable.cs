using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using System.Linq.Expressions;
using System.Reflection;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class DomainNotifiable<TEntity> : IDomainNotifiable
{
    protected NotificationInfo NotificationInfo { get; set; }

    public DomainNotifiable()
    {
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
    /// <param name="context"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void SetNotificationContext(NotificationContext context)
    {
        NotificationContext = context;
    }

    protected List<NotificationModel> Notifications => NotificationContext.Notifications.Where(a => a.context == NotificationInfo.Namespace).ToList();

    /// <summary>
    /// Indica se o dominio é válido ou não
    /// </summary>
    /// <returns></returns>
    public bool IsValid()
    {
        return !Notifications.Any();
    }

    /// <summary>
    /// Instancia classe para trabalhar com notificationPattern
    /// </summary>
    /// <typeparam name="TNotifiable"></typeparam>
    /// <returns></returns>
    protected TNotifiable Notifiable<TNotifiable>() where TNotifiable : INotifiable
    {
        var entity = Activator.CreateInstance<TNotifiable>();
        entity.SetNotificationContext(NotificationContext);
        return entity;
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void SetAggregateRoot(bool value)
    {
        this.NotificationInfo.MainDomain = true;
    }
}