namespace Marketplace.Domain;

public abstract class AggregateRoot<TEntityId>
{
    public TEntityId Id { get; protected set; }
    protected abstract void EnsureValidState();
    protected abstract void When(object @event);

    protected void Apply(object @event)
    {
        When(@event);
        EnsureValidState();
    }
}