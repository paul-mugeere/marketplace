using Marketplace.Domain.ClassifiedAds.Events;
using Marketplace.Domain.ClassifiedAds.Exceptions;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds;

public class ClassifiedAd : AggregateRoot<ClassifiedAdId>
{
    public UserId OwnerId { get; private set; }
    public ClassifiedAdTitle Title { get; private set; }
    public ClassifiedAdText Text { get; private set; }
    
    public ClassifiedAdState State { get; private set; }
    public Price Price { get; private set; }
    
    public UserId ApprovedBy { get; private set; }

    public List<Picture> Pictures { get; private set; } = [];

    public void SetTitle(ClassifiedAdTitle title) => Apply(new ClassifiedAdTitleChanged(Id, Title));

    public void UpdateText(ClassifiedAdText text)=> Apply(new ClassifiedAdTextChanged(Id, text));

    public void UpdatePrice(Price price) => Apply(new ClassifiedAdPriceUpdated(Id, price));

    public void RequestToPublish() => Apply(new ClassifiedAdSentForReview(Id));
    
    public void AddPicture(Uri pictureUri, PictureSize size) => Apply(new PictureAddedToClassifiedAd(Id, PictureId.CreateNew(), pictureUri, size));


    public static ClassifiedAd CreateNew(UserId ownerId) => new(ClassifiedAdId.CreateNew(),ownerId,ClassifiedAdState.Inactive);
    public static ClassifiedAd CreateNew(UserId ownerId,ClassifiedAdState state) => new(ClassifiedAdId.CreateNew(),ownerId,state);
    
    private ClassifiedAd(){}

    private ClassifiedAd(
        ClassifiedAdId id,
        UserId ownerId,
        ClassifiedAdState state) => Apply(new ClassifiedAdCreated(id, ownerId, state));
    
    protected override void EnsureValidState()
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

    protected override void When(object @event)
    {
        switch (@event)
        {
            case ClassifiedAdCreated createdEvent:
                Id = createdEvent.Id;
                OwnerId = createdEvent.OwnerId;
                State = createdEvent.State;
                break;
            case ClassifiedAdTitleChanged titleEvent:
                Title = titleEvent.Title;
                break;
            case ClassifiedAdTextChanged textEvent:
                Text = textEvent.Text;
                break;
            case ClassifiedAdPriceUpdated priceUpdatedEvent:
                Price = priceUpdatedEvent.Price;
                break;
            case ClassifiedAdSentForReview sentForReviewEvent:
                State = ClassifiedAdState.PendingReview;
                break;
            case PictureAddedToClassifiedAd pictureAddedEvent:
                var picture = Picture.CreateNew(pictureAddedEvent.Url,pictureAddedEvent.Size, Pictures.Max(x=>x.Order) + 1);
                Pictures.Add(picture);
                break;
        }
    }
}