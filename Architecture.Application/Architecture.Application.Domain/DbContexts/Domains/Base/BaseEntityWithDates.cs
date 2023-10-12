namespace Architecture.Application.Domain.DbContexts.Domains.Base;

public class BaseEntityWithDates<TEntity> : BaseEntity<TEntity>
{
    public DateTime CreateDate { get; private set; }

    public DateTime? UpdateDate { get; private set; }
}
