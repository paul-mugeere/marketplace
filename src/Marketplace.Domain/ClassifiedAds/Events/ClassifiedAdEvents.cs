using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds.Events;

public record ClassifiedAdTitleChanged(ClassifiedAdId Id, ClassifiedAdTitle Title);
public record ClassifiedAdTextChanged(ClassifiedAdId Id, ClassifiedAdText Text);
public record ClassifiedAdPriceUpdated(ClassifiedAdId Id, Price Price);
public record ClassifiedAdSentForReview(ClassifiedAdId Id);
public record ClassifiedAdCreated(ClassifiedAdId Id, UserId OwnerId, ClassifiedAdState State);
public record PictureAddedToClassifiedAd(ClassifiedAdId Id, PictureId PictureId, Uri Url, PictureSize Size, int Order);
public record ClassifiedAdPictureResized(ClassifiedAdId Id, PictureId PictureId, PictureSize Size);