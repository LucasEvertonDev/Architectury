using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using System.Linq.Expressions;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class DomainNotifiable<TEntity> : IDomainNotifiable
{
    /// <summary>
    ///  Quando record as notificaçoes são integradas de forma interna 
    /// </summary>
    /// <param name="memberLamda"></param>
    /// <param name="value"></param>
    protected void Set(Expression<Func<TEntity, IDomainNotifiable>> memberLamda, IDomainNotifiable value)
    {
        this.NotificationContext.AddNotifications(value.GetNotifications());

        this.SetValue(memberLamda, value);
    }
}