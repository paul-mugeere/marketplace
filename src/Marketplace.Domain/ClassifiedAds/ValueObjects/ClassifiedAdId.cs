namespace Marketplace.Domain.ClassifiedAds.ValueObjects;

public record ClassifiedAdId
{
    public Guid Value { get; }

    private ClassifiedAdId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentException($"'{nameof(value)}' ClassifiedAd Id must be specified.", nameof(value));
        }
        Value = value;
    }
    
    public static ClassifiedAdId CreateNew() => new(Guid.NewGuid());
    public static ClassifiedAdId FromGuid(Guid value) => new(value);
}