using Marketplace.Domain.ClassifiedAds.Services;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects;

public record Price: Money
{
    private Price(decimal amount,string currencyCode,ICurrencyLookup currencyLookup) : base(amount,currencyCode, currencyLookup)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);
    }
    public new static Price FromDecimal(decimal amount,string currencyCode,ICurrencyLookup currencyLookup) => new(amount,currencyCode,currencyLookup);
}