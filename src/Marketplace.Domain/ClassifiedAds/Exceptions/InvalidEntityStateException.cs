namespace Marketplace.Domain.ClassifiedAds.Exceptions;

public class InvalidEntityStateException(object entity, string message)
    : Exception($"Entity {entity.GetType().Name} state change is invalid. {message}");