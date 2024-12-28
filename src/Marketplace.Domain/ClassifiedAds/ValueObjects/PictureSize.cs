namespace Marketplace.Domain.ClassifiedAds.ValueObjects;

public record PictureSize
{
    public int Width { get; }
    public int Height { get; }

    private PictureSize(int width, int height)
    {
        if (width<=0) throw new ArgumentOutOfRangeException(nameof(width), "Picture Width must be greater than zero");
        if (height<=0) throw new ArgumentOutOfRangeException(nameof(width), "Picture Width must be greater than zero");
        
        Width = width;
        Height = height;
    }
    
    public static PictureSize CreateNew(int width, int height) => new (width, height);
    
    private PictureSize() { }
    
}