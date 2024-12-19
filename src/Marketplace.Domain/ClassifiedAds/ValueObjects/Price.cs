namespace Marketplace.Domain.ClassifiedAds.ValueObjects;

public record Price: Money
{
    private Price(decimal amount) : base(amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);
    }
    public new static Price FromDecimal(decimal amount) => new(amount);
}