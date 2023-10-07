namespace Architecture.Application.Core.Structure.Domain;

public abstract class BasicDomain
{
    public Guid Id { get; protected set; }

    public int Situation { get; protected set; }
}
