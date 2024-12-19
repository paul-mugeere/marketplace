using FluentAssertions;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.Tests;

public class ClassifiedAdTitleTests
{
    [Fact]
    public void ClassifiedAddText_can_not_be_longer_than_100_characters()
    {
        var text = new string('a', 1001);
        Action action = () => ClassifiedAdTitle.FromString(text);
        action.Should().Throw<ArgumentException>();
    }
}