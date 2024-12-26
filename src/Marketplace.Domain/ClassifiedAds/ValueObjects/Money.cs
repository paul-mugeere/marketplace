using Marketplace.Domain.ClassifiedAds.Exceptions;
using Marketplace.Domain.ClassifiedAds.Services;

namespace Marketplace.Domain.ClassifiedAds.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public Currency Currency { get; init; }

    protected Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
    {
        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            throw new ArgumentNullException(nameof(currencyCode), "Currency code must be specified");
        }
        
        var currency = currencyLookup.Find(currencyCode);
        if (!currency.InUse)
        {
            throw new ArgumentException($"Currency code {currencyCode} is not valid");
        }

        if (decimal.Round(amount,currency.Decimals) !=amount)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), $"Amount in {currencyCode} cannot have more than {currency.Decimals} decimals");
        }
        
        Amount = amount;
        Currency = currency;
    }
    public static Money FromDecimal(decimal amount,string currency,ICurrencyLookup currencyLookup) => new(amount, currency,currencyLookup);

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

    public override string ToString()=> $"{Currency.Code} {Amount}";
}