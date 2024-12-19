using Marketplace.Domain.ClassifiedAds.Exceptions;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }

    protected Money(decimal amount, string currency="EUR")
    {
        Amount = amount;
        Currency = currency;
    }
    public static Money FromDecimal(decimal amount,string currency="") => new(amount, currency);

    public Money Add(Money money)
    {
        if (money.Currency != Currency) throw new CurrencyMismatchException("Cannot add amounts with different currencies");
        return this with { Amount = Amount + money.Amount };
    }

    public Money Subtract(Money money)
    {
        if (money.Currency != Currency) throw new CurrencyMismatchException("Cannot subtract amounts with different currencies");
        return this with { Amount = Amount - money.Amount };
    }
}