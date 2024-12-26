namespace Marketplace.Domain.ClassifiedAds.ValueObjects;

public record ClassifiedAdTitle
{
    public string Value { get;}

    private ClassifiedAdTitle(string value)
    {
        if (value.Length > 100)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"'{nameof(value)}' Classified Add Text must longer than 100 characters.");
        }
        Value = value;
    }

    public static ClassifiedAdTitle FromString(string text) => new(text);
}