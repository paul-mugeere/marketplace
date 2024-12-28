using Marketplace.Domain.ClassifiedAds.Events;
using Marketplace.Domain.ClassifiedAds.ValueObjects;

namespace Marketplace.Domain.ClassifiedAds;

public class Picture : Entity<PictureId>
{
    internal PictureSize Size { get; private set; }
    internal Uri Location { get; private set; }
    internal int Order { get; private set; }
   
    private Picture(Uri url, PictureSize size, int order)
    {
        Location = url ?? throw new ArgumentNullException(nameof(url));
        Size = size;
        Order = order;
    }

    private Picture()
    {
    }

    public static Picture CreateNew(Uri url, PictureSize size, int order) => new (url, size, order);

    public void Resize(PictureSize newSize)
    {
        Size = newSize;
    }
}