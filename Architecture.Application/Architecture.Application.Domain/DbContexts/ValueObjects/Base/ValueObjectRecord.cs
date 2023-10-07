using Architecture.Application.Core.Notifications.Notifiable.Notifications;
using Architecture.Application.Core.Notifications.Notifiable.Notifications.Base;

namespace Architecture.Application.Domain.DbContexts.ValueObjects.Base;

public record ValueObjectRecord<TEntity> : RecordNotifiable<TEntity>, IRecordNotifiable
{
}
