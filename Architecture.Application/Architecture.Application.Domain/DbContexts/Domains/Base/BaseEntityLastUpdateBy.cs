namespace Architecture.Application.Domain.DbContexts.Domains.Base;

public class BaseEntityLastUpdateBy<TEntity> : BaseEntityWithDates<TEntity>
{
    public string LastUpdateBy { get; set; }
}
