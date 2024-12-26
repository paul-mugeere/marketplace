using Marketplace.Domain.ClassifiedAds.Services;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.Tests;

public class FakeCurrencyLookup : ICurrencyLookup
{
    private IEnumerable<Currency> _currencies =
    [
        new()
        {
            InUse = true,
            Code = "EUR",
            Decimals = 2
        },
        new ()
        {
            InUse = true,
            Code = "USD",
            Decimals = 2
        }
    ];
    public Currency Find(string currencyCode)
    {
        return _currencies.FirstOrDefault(x => x.Code == currencyCode)?? Currency.None;
    }
}