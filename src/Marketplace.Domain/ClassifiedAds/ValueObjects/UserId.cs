namespace Marketplace.Domain.ClassifiedAds.ValueObjects;

public record UserId
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        if (value == default)
        {
            throw new ArgumentException($"'{nameof(value)}' User Id must be specified.", nameof(value));
        }
        
        Value = value;
    }

    public static UserId CreateNew() => new (Guid.NewGuid());
    public static UserId FromGuid(Guid value) => new(value);
}