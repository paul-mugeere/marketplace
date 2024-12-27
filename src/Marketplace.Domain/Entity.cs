namespace Marketplace.Domain;

public abstract class Entity
{
    protected abstract void EnsureValidState();
    protected abstract void When(object @event);

    protected void Apply(object @event)
    {
        When(@event);
        EnsureValidState();
    }
    
}