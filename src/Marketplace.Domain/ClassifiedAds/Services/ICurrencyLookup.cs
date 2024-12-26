using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds.Services;

public interface ICurrencyLookup
{
    Currency Find(string currencyCode);
}