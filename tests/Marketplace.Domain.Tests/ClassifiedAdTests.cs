using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using Marketplace.Domain.ClassifiedAds;
using Marketplace.Domain.ClassifiedAds.Exceptions;
using Marketplace.Domain.ClassifiedAds.Services;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.Tests;

public class ClassifiedAdTests
{
    
    private readonly Fixture _fixture;
    private readonly ICurrencyLookup _currencyLookup;

    public ClassifiedAdTests()
    {
        _fixture = new Fixture();
        _currencyLookup = A.Fake<ICurrencyLookup>();
    }
    
    [Fact]
    public void GivenAnActiveClassifiedAd_WithInvalidState_ShouldThrowInvalidEntityException()
    {
        var userId = _fixture.Create<UserId>();
        Action action = () => ClassifiedAd.CreateNew(userId,ClassifiedAdState.Active);
        action.Should().Throw<InvalidEntityStateException>();
    }

    [Fact]
    public void GivenValidClassifiedAd_RequestToPublish_ShouldPublishClassifiedAdd()
    {
        A.CallTo(() => _currencyLookup.Find(A<string>._)).Returns(new Currency()
        {
            Code = "USD",
            Decimals = 2,
            InUse = true
        });
        var addToPublish = ClassifiedAd.CreateNew(_fixture.Create<UserId>());
        addToPublish.SetTitle(_fixture.Create<ClassifiedAdTitle>());
        var price = Price.FromDecimal(_fixture.Create<decimal>(),"USD",_currencyLookup);
        addToPublish.UpdatePrice(price);
        addToPublish.UpdateText(_fixture.Create<ClassifiedAdText>());
        
        addToPublish.RequestToPublish();
        
        addToPublish.State.Should().Be(ClassifiedAdState.PendingReview);
    }

    [Fact]
    public void GivenClassifiedAdWithNoTitle_RequestToPublish_ShouldThrowInvalidEntityException()
    {
        A.CallTo(() => _currencyLookup.Find(A<string>._)).Returns(new Currency()
        {
            Code = "USD",
            Decimals = 2,
            InUse = true
        });
        var addToPublish = ClassifiedAd.CreateNew(_fixture.Create<UserId>());
        var price = Price.FromDecimal(_fixture.Create<decimal>(),"USD",_currencyLookup);
        addToPublish.UpdatePrice(price);
        addToPublish.UpdateText(_fixture.Create<ClassifiedAdText>());
        
        var action = () => addToPublish.RequestToPublish();
        
        action.Should().Throw<InvalidEntityStateException>();
    }
}