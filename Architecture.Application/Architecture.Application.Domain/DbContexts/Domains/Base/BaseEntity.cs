using Architecture.Application.Domain.Enuns;
using Notifications.Notifiable.Notifications;

namespace Architecture.Application.Domain.DbContexts.Domains.Base;

public partial class BaseEntity<TEntity> : Notifiable<TEntity>, IEntity
{
    public BaseEntity()
    {
    }

    public Guid Id { get; protected set; }

    public int Situacao { get; protected set; }

    public void Delete()
    {
        this.Situacao = (int)ESituacao.Excluido;
    }
}
