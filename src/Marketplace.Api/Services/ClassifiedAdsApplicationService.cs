using Marketplace.Api.Contracts;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.Services;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Api.Services;

public class ClassifiedAdsApplicationService
    (IEntityStore entityStore, ICurrencyLookup currencyLookup)
    : IClassifiedAdsApplicationService
{
    public Task Handle(object command) => command switch
    {
        ClassifiedAds.V1.Create cmd => CreateClassifiedAdAsync(cmd),
        ClassifiedAds.V1.SetTitle cmd => UpdateClassifiedAdAsync(cmd.Id, c => c.SetTitle(ClassifiedAdTitle.FromString(cmd.Title))),
        ClassifiedAds.V1.UpdateText cmd => UpdateClassifiedAdAsync(cmd.Id, c => c.UpdateText(ClassifiedAdText.FromString(cmd.Text))),
        ClassifiedAds.V1.UpdatePrice cmd => UpdateClassifiedAdAsync(cmd.Id, c => c.UpdatePrice(Price.FromDecimal(cmd.Price, cmd.Currency,currencyLookup))),
        ClassifiedAds.V1.RequestToPublish cmd => UpdateClassifiedAdAsync(cmd.Id, c => c.RequestToPublish()),
        _ => throw new InvalidOperationException($"Command {command.GetType().FullName} is not supported")
    };
    
    private async Task UpdateClassifiedAdAsync(Guid id, Action<ClassifiedAd> executeClassifiedAdOperation)
    {
        var ad = await entityStore.Load<ClassifiedAd>(id.ToString());
        if (ad == null)
        {
            throw new InvalidOperationException($"Classified ad with id {id} does not exist");
        }
        
        executeClassifiedAdOperation(ad);
        await entityStore.Save(ad);
    }
    
    private async Task CreateClassifiedAdAsync(ClassifiedAds.V1.Create command)
    {
        var ad = ClassifiedAd.CreateNew(UserId.FromGuid(command.OwnerId));
        await entityStore.Save(ad);
    }
}

public interface IClassifiedAdsApplicationService
{
    Task Handle(object command);
}