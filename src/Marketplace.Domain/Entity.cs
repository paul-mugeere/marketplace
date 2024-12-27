namespace Marketplace.Domain;

public abstract class Entity<TEntityId>
{
    public TEntityId Id { get; protected set; }
    protected abstract void When(object @event);

    protected void Apply(object @event)
    {
        When(@event);
    }
}