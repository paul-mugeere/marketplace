using FakeItEasy;
using FluentAssertions;
using Marketplace.Domain.ClassifiedAds.Exceptions;
using Marketplace.Domain.ClassifiedAds.Services;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.Tests;

public class MoneyTests
{
    private readonly ICurrencyLookup _currencyLookup;

    public MoneyTests()
    {
        _currencyLookup = new FakeCurrencyLookup();
    }
    
    [Fact]
    public void MoneyObjects_WithSameValues_AreEqual()
    {
        var firstAmount = Money.FromDecimal(100m,"EUR",_currencyLookup);
        var secondAmount = Money.FromDecimal(100m,"EUR",_currencyLookup);
        firstAmount.Should().Be(secondAmount);
    }

    [Fact]
    public void Price_WithNegativeAmount_ShouldThrowException()
    {
        var amount = -100m;
        Action action = () => Price.FromDecimal(amount,"EUR",_currencyLookup);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void GivenMoney_AddAmount_ShouldAddCorrectAmount()
    {
        var amount = Money.FromDecimal(100m,"EUR",_currencyLookup);
        var sum = amount.Add(Money.FromDecimal(100m,"EUR",_currencyLookup));
        sum.Amount.Should().Be(200m);
    }

    [Fact]
    public void GivenMoney_AddAmountWithDifferentCurrencies_shouldThrowCurrencyMismatchException()
    {
        var amount = Money.FromDecimal(100m,"EUR",_currencyLookup);
        var amountToadd = Money.FromDecimal(100m, "USD", _currencyLookup);
        Action sumAction = () => amount.Add(amountToadd);
        sumAction.Should().Throw<CurrencyMismatchException>();
    }

    [Fact]
    public void GivenMoney_SubtractAmount_ShouldSubtractCorrectAmount()
    {
        var amount = Money.FromDecimal(100m,"EUR",_currencyLookup);
        var sum = amount.Subtract(Money.FromDecimal(50m,"EUR",_currencyLookup));
        sum.Amount.Should().Be(50m);
    }

    [Fact]
    public void GivenMoney_SubtractAmountWithDifferentCurrencies_shouldThrowCurrencyMismatchException()
    {
        var amount = Money.FromDecimal(100m, "EUR",_currencyLookup);
        Action sumAction = () => amount.Subtract(Money.FromDecimal(100m,"USD",_currencyLookup));
        sumAction.Should().Throw<CurrencyMismatchException>();
    }
}