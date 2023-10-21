namespace Architecture.Application.Domain.DbContexts.Domains.Base;

public class BaseEntityWithDates<TEntity> : BaseEntity<TEntity>
{
    public DateTime CreateDate { get; protected set; }

    public DateTime? UpdateDate { get; protected set; }
}
