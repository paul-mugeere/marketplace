namespace Marketplace.Domain;

public abstract class Entity<TEntityId>
{
    public TEntityId Id { get; protected set; }
}