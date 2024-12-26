using Marketplace.Api.Contracts;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.Services;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Api.Services;

public class ClassifiedAdsApplicationService
    (IEntityStore entityStore, ICurrencyLookup currencyLookup)
    : IHandleCommand<object>
{
    public async Task Handle(object command)
    {
        switch (command)
        {
            case  ClassifiedAds.V1.Create create:
                await CreateClassifiedAdAsync(create);
                break;
            case ClassifiedAds.V1.SetTitle setTitle:
                await SetClassifiedAdTitleAsync(setTitle);
                break;
            case ClassifiedAds.V1.UpdatePrice updatePrice:
                await UpdateClassifiedAdPriceAsync(updatePrice);
                break;
            case ClassifiedAds.V1.UpdateText updateText:
                await UpdateClassifiedAdTextAsync(updateText);
                break;
            case ClassifiedAds.V1.RequestToPublish requestToPublish:
                await RequestToPublishClassifiedAdAsync(requestToPublish);
                break;
            default:
                throw new InvalidOperationException($"Command {command.GetType().FullName} is not supported");
        }
    }

    private async Task RequestToPublishClassifiedAdAsync(ClassifiedAds.V1.RequestToPublish command)
    {
        var ad = await entityStore.Load<ClassifiedAd>(command.Id.ToString());
        if (ad == null)
        {
            throw new InvalidOperationException($"Classified ad with id {command.Id} does not exist");
        }
        
        ad.RequestToPublish();
    }

    private async Task UpdateClassifiedAdTextAsync(ClassifiedAds.V1.UpdateText command)
    {
        var ad = await entityStore.Load<ClassifiedAd>(command.Id.ToString());
        if (ad == null)
        {
            throw new InvalidOperationException($"Classified ad with id {command.Id} does not exist");
        }
        
        ad.UpdateText(ClassifiedAdText.FromString(command.Text));
    }

    private async Task UpdateClassifiedAdPriceAsync(ClassifiedAds.V1.UpdatePrice command)
    {
        var ad = await entityStore.Load<ClassifiedAd>(command.Id.ToString());
        if (ad == null)
        {
            throw new InvalidOperationException($"Classified ad with id {command.Id} does not exist");
        }
        
        ad.UpdatePrice(Price.FromDecimal(command.Price,command.Currency,currencyLookup));
    }

    private async Task SetClassifiedAdTitleAsync(ClassifiedAds.V1.SetTitle command)
    {
        var ad = await entityStore.Load<ClassifiedAd>(command.Id.ToString());
        if (ad == null)
        {
            throw new InvalidOperationException($"Classified ad with id {command.Id} does not exist");
        }
        
        ad.SetTitle(ClassifiedAdTitle.FromString(command.Title));
        await entityStore.Save(ad);
    }

    private async Task CreateClassifiedAdAsync(ClassifiedAds.V1.Create command)
    {
        var ad = ClassifiedAd.CreateNew(UserId.FromGuid(command.OwnerId));
        await entityStore.Save(ad);
    }
}