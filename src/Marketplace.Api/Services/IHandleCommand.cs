namespace Marketplace.Api.Services;

public interface IHandleCommand<in TCommand>
{
    Task Handle(TCommand command);
}
