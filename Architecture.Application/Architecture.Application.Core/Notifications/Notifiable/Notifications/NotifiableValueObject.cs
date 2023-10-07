using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using System.Linq.Expressions;
using System.Reflection;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class DomainNotifiable<TEntity> : IDomainNotifiable
{
    /// <summary>
    ///  Quando record as notificaçoes são integradas de forma interna 
    /// </summary>
    /// <param name="memberLamda"></param>
    /// <param name="value"></param>
    protected void Set(Expression<Func<TEntity, IRecordNotifiable>> memberLamda, IRecordNotifiable value)
    {
        this.SetValue(memberLamda, value);
    }

    /// <summary>
    ///  Quando record as notificaçoes são integradas de forma interna 
    /// </summary>
    /// <param name="memberLamda"></param>
    /// <param name="value"></param>
    protected void Set(Expression<Func<TEntity, IDomainNotifiable>> memberLamda, IDomainNotifiable value)
    {
        this.SetValue(memberLamda, value);
    }
}

public partial record RecordNotifiable<TEntity> : IRecordNotifiable
{
    /// <summary>
    ///  Quando record as notificaçoes são integradas de forma interna 
    /// </summary>
    /// <param name="memberLamda"></param>
    /// <param name="value"></param>
    protected void Set(Expression<Func<TEntity, IRecordNotifiable>> memberLamda, IRecordNotifiable value)
    {
        this.SetValue(memberLamda, value);
    }
}
