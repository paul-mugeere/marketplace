namespace Marketplace.Domain;

public interface IInternalEventHandler
{
    void Handle(object @event);
}