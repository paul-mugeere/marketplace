using Marketplace.Api.Contracts;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Api.Services;

public class CreateClassifiedAdCommandHandler : IHandleCommand<ClassifiedAds.V1.Create>
{
    private readonly IEntityStore _entityStore;

    public CreateClassifiedAdCommandHandler(IEntityStore entityStore)
    {
        _entityStore = entityStore;
    }

    public async Task Handle(ClassifiedAds.V1.Create command)
    {
        var ad = ClassifiedAd.CreateNew(UserId.FromGuid(command.OwnerId));
        await _entityStore.Save(ad);
    }
}

public class SetClassifiedAdTitleCommandHandler : IHandleCommand<ClassifiedAds.V1.SetTitle>
{
    private readonly IEntityStore _entityStore;

    public SetClassifiedAdTitleCommandHandler(IEntityStore entityStore)
    {
        _entityStore = entityStore;
    }

    public async Task Handle(ClassifiedAds.V1.SetTitle command)
    {
        var ad = await _entityStore.Load<ClassifiedAd>(command.Id.ToString());
        if (ad == null)
        {
            throw new InvalidOperationException($"Classified ad with id {command.Id} does not exist");
        }
        ad.SetTitle(ClassifiedAdTitle.FromString(command.Title));
        await _entityStore.Save(ad);
    }
}

public class UpdateClassifiedAdTextCommandHandler : IHandleCommand<ClassifiedAds.V1.UpdateText>
{ 
    private readonly IEntityStore _entityStore;

    public UpdateClassifiedAdTextCommandHandler(IEntityStore entityStore)
    {
        _entityStore = entityStore;
    }

    public async Task Handle(ClassifiedAds.V1.UpdateText command)
    {
        var ad = await _entityStore.Load<ClassifiedAd>(command.Id.ToString());
        if (ad == null)
        {
            throw new InvalidOperationException($"Classified ad with id {command.Id} does not exist");
        }
        ad.UpdateText(ClassifiedAdText.FromString(command.Text));
        await _entityStore.Save(ad);
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

