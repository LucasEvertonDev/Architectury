﻿using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterSet;
using Architecture.Application.Core.Notifications.Notifiable.Steps.AfterValidationWhen;
using System.Linq.Expressions;
using System.Reflection;

namespace Architecture.Application.Core.Notifications.Notifiable.Notifications;

public partial class Notifiable<TEntity> : INotifiableModel
{
    protected AfterSet<AfterValidationWhenString> Set(Expression<Func<TEntity, string>> memberLamda, string value)
    {
        this.SetValue(memberLamda, value);

        return new AfterSet<AfterValidationWhenString>(NotificationContext, NotificationInfo);
    }
}