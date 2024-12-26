namespace Marketplace.Domain.ClassifiedAds.ValueObjects;

public record Currency
{
    public string Code { get; init; }
    public bool InUse { get; init; }
    public int Decimals { get; init; }
    
    public static Currency None => new (){InUse = false};
}