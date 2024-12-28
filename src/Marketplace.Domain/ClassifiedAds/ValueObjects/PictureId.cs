namespace Marketplace.Domain.ClassifiedAds.ValueObjects;

public record PictureId
{
    public Guid Value { get;}
    
    private PictureId(Guid value) => Value = value;
    public static PictureId CreateNew() => new(Guid.NewGuid());
    public static PictureId FromGuiId(Guid value) => new(value);
}