namespace Marketplace.Api.Services;

public interface IEntityStore
{
    Task<T> Load<T>(string id);
    Task Save<T>(T value);
}

public class RavenDbEntityStore : IEntityStore
{
    public Task<T> Load<T>(string id)
    {
        throw new NotImplementedException();
    }

    public Task Save<T>(T value)
    {
        throw new NotImplementedException();
    }
}