using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds.Extensions;

public static class PictureRules
{
    public static bool HasCorrectSize(this Picture? picture) => picture?.Size is { Width: >= 800, Height: >= 600 };
}