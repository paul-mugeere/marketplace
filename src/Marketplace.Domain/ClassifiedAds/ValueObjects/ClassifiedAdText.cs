namespace Marketplace.Domain.ClassifiedAds.ValueObjects;

public record ClassifiedAdText
{
    public string Value { get;}

    private ClassifiedAdText(string value)
    {
        Value = value;
    }

    public static ClassifiedAdText FromString(string text) => new(text);
}