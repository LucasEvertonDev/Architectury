namespace Architecture.Application.Domain.DbContexts.Domains.Base;

public abstract class BasicDomain : IEntity
{
    public Guid Id { get; protected set; }

    public int Situation { get; protected set; }
}
