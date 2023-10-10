using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using System.Linq.Expressions;
using System.Reflection;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable<TEntity> : INotifiableModel
{
    protected NotificationInfo NotificationInfo { get; set; }

    protected Result Result { get; set; }

    public Notifiable()
    {
        Result = new Result(new NotificationContext());

        NotificationInfo = new NotificationInfo()
        {
            NotificationType = Enum.NotificationType.DomainNotification,
            Name = typeof(TEntity).Name,
            Namespace = typeof(TEntity).Namespace
        };
    }

    /// <summary>
    /// 
    /// </summary>
    public List<NotificationModel> GetFailures()
    {
        return Result.GetContext().Notifications.ToList();
    }

    /// <summary>
    /// Indica se o dominio é válido ou não
    /// </summary>
    /// <returns></returns>
    public bool HasFailure()
    {
        return GetFailures().Any();
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
                NotificationInfo.MemberName = value is INotifiableModel ? NotificationInfo.Name :  string.Concat(NotificationInfo.Name, ".", property.Name);
            }
        }
        NotificationInfo.Value = value;
    }
}