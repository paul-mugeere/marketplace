using Marketplace.Api.Contracts;

namespace Marketplace.Api.Services;

public class CreateClassifiedAdCommandHandler : IHandleCommand<ClassifiedAds.V1.Create>
{
    private readonly IEntityStore _entityStore;

    public CreateClassifiedAdCommandHandler(IEntityStore entityStore)
    {
        _entityStore = entityStore;
    }

    public Task Handle(ClassifiedAds.V1.Create command)
    {
        throw new NotImplementedException();
    }
}

public class SetClassifiedAdTitleCommandHandler : IHandleCommand<ClassifiedAds.V1.SetTitle>
{
    private readonly IEntityStore _entityStore;

    public SetClassifiedAdTitleCommandHandler(IEntityStore entityStore)
    {
        _entityStore = entityStore;
    }

    public Task Handle(ClassifiedAds.V1.SetTitle command)
    {
        throw new NotImplementedException();
    }
}

public class UpdateClassifiedAdTextCommandHandler : IHandleCommand<ClassifiedAds.V1.UpdateText>
{
    public Task Handle(ClassifiedAds.V1.UpdateText command)
    {
        throw new NotImplementedException();
    }
}

public class UpdateClassifiedAdPriceCommandHandler : IHandleCommand<ClassifiedAds.V1.UpdatePrice>
{
    private readonly IEntityStore _entityStore;

    public UpdateClassifiedAdPriceCommandHandler(IEntityStore entityStore)
    {
        _entityStore = entityStore;
    }

    public Task Handle(ClassifiedAds.V1.UpdatePrice command)
    {
        throw new NotImplementedException();
    }
}

