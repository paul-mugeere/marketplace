using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds;

public class ClassifiedAd
{
    public ClassifiedAdId Id { get; private set; }
    private UserId _ownerId;
    private ClassifiedAdTitle _title;
    private string _text;
    private Price _price;

    public void SetTitle(ClassifiedAdTitle title) =>_title = title;
    public void UpdateText(string text) =>_text = text;
    public void UpdatePrice(Price price) =>_price = price;
    
    public static ClassifiedAd CreateNew(UserId ownerId) => new(ClassifiedAdId.CreateNew(),ownerId);
    
    private ClassifiedAd(){}
    private ClassifiedAd(ClassifiedAdId id, UserId ownerId)
    {
        _ownerId = ownerId;
        Id = id;
    }
}