using Marketplace.Domain.ClassifiedAds.Exceptions;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds;

public class ClassifiedAd
{
    public ClassifiedAdId Id { get; private set; }
    public UserId OwnerId { get; }
    public ClassifiedAdTitle Title { get; private set; }
    public ClassifiedAdText Text { get; private set; }
    
    public ClassifiedAdState State { get; private set; }
    public Price Price { get; private set; }
    
    public UserId ApprovedBy { get; private set; }

    public void SetTitle(ClassifiedAdTitle title)
    {
        Title = title;
        EnsureValidState();
    }

    public void UpdateText(ClassifiedAdText text)
    {
        Text = text;
        EnsureValidState();
    }

    public void UpdatePrice(Price price)
    {
        Price = price;
        EnsureValidState();
    }

    public void RequestToPublish()
    {
        State = ClassifiedAdState.PendingReview;
        EnsureValidState();
    }


    public static ClassifiedAd CreateNew(UserId ownerId) => new(ClassifiedAdId.CreateNew(),ownerId,ClassifiedAdState.Inactive);
    public static ClassifiedAd CreateNew(UserId ownerId,ClassifiedAdState state) => new(ClassifiedAdId.CreateNew(),ownerId,state);
    
    private ClassifiedAd(){}
    private ClassifiedAd(
        ClassifiedAdId id, 
        UserId ownerId,
        ClassifiedAdState state)
    {
        OwnerId = ownerId;
        Id = id;
        State = state;
        EnsureValidState();
    }
    
    private void EnsureValidState()
    {
        var isValidState = Id?.Value != default && OwnerId?.Value != default && (State switch
        {
            ClassifiedAdState.PendingReview => 
                !string.IsNullOrWhiteSpace(Title?.Value) 
                && !string.IsNullOrWhiteSpace(Text?.Value) && Price?.Amount > 0,
            ClassifiedAdState.Active => 
                !string.IsNullOrWhiteSpace(Title?.Value)
                && !string.IsNullOrWhiteSpace(Text?.Value) && Price?.Amount > 0
                && ApprovedBy?.Value != default,
            _ => true
        });

        if(isValidState is not true)
        {
            throw new InvalidEntityStateException(this, "Title cannot be empty.");
        }
    }

}