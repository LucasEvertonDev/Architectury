using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using System.Linq.Expressions;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable<TEntity> : INotifiableModel
{
    /// <summary>
    ///  Quando record as notificaçoes são integradas de forma interna 
    /// </summary>
    /// <param name="memberLamda"></param>
    /// <param name="value"></param>
    protected void Set(Expression<Func<TEntity, INotifiableModel>> memberLamda, INotifiableModel value)
    {
        this.SetValue(memberLamda, value);

        for (var i = 0; i < value.GetFailures().Count(); i++)
        {
            var failure = value.GetFailures()[i];

            failure.SetMemberNamePrefix(NotificationInfo.MemberName);

            this.Result.GetContext().AddNotification(failure);
        }
    }
}