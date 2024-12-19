using FluentAssertions;
using Marketplace.Domain.ClassifiedAds.Exceptions;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.Tests;

public class MoneyTests
{
    [Fact]
    public void MoneyObjects_WithSameValues_AreEqual()
    {
        var firstAmount = Money.FromDecimal(100m);
        var secondAmount = Money.FromDecimal(100m);
        firstAmount.Should().Be(secondAmount);
    }

    [Fact]
    public void Price_WithNegativeAmount_ShouldThrowException()
    {
        var amount = -100m;
        Action action = () => Price.FromDecimal(amount);
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void GivenMoney_AddAmount_ShouldAddCorrectAmount()
    {
        var amount = Money.FromDecimal(100m);
        var sum = amount.Add(Money.FromDecimal(100m));
        sum.Amount.Should().Be(200m);
    }

    [Fact]
    public void GivenMoney_AddAmountWithDifferentCurrencies_shouldThrowCurrencyMismatchException()
    {
        var amount = Money.FromDecimal(100m, "EUR");
        Action sumAction = () => amount.Add(Money.FromDecimal(100m, "USD"));
        sumAction.Should().Throw<CurrencyMismatchException>();
    }

    [Fact]
    public void GivenMoney_SubtractAmount_ShouldSubtractCorrectAmount()
    {
        var amount = Money.FromDecimal(100m);
        var sum = amount.Subtract(Money.FromDecimal(50m));
        sum.Amount.Should().Be(50m);
    }

    [Fact]
    public void GivenMoney_SubtractAmountWithDifferentCurrencies_shouldThrowCurrencyMismatchException()
    {
        var amount = Money.FromDecimal(100m, "EUR");
        Action sumAction = () => amount.Subtract(Money.FromDecimal(100m, "USD"));
        sumAction.Should().Throw<CurrencyMismatchException>();
    }
}