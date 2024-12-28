using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds;

public class Picture : Entity<PictureId>
{

    internal PictureSize Size { get; }
    internal Uri Location { get; }
    internal int Order { get; }
    protected override void When(object @event)
    {
        throw new NotImplementedException();
    }
    
    private Picture(Uri url, PictureSize size, int order)
    {
        Location = url ?? throw new ArgumentNullException(nameof(url));
        Size = size;
        Order = order;
    }
    public static Picture CreateNew(Uri url, PictureSize size, int order) => new (url, size, order);
}