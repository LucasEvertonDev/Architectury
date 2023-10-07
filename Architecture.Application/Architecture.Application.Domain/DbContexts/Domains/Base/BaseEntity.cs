using Architecture.Application.Core.Notifications.Notifiable.Notifications;
using Architecture.Application.Domain.Enuns;

namespace Architecture.Application.Domain.DbContexts.Domains.Base;

public partial class BaseEntity<TEntity> : DomainNotifiable<TEntity>, IEntity
{
    public Guid Id { get; protected set; }

    public int Situacao { get; protected set; }


    public void Delete()
    {
        this.Situacao = (int)ESituacao.Excluido;
    }
}
