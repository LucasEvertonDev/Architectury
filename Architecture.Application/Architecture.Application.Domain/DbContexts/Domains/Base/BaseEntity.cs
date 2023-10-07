using Architecture.Application.Core.Notifications.Notifiable.Notifications;

namespace Architecture.Application.Domain.DbContexts.Domains.Base;

public partial class BaseEntity<TEntity> : DomainNotifiable<TEntity>, IEntity
{
    public Guid Id { get; protected set; }

    public int Situation { get; protected set; }
}
