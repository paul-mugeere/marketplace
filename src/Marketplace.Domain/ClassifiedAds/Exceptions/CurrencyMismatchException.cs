namespace Marketplace.Domain.ClassifiedAds.Exceptions;

public class CurrencyMismatchException(string? message) : Exception(message);