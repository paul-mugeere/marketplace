namespace Marketplace.Api.Contracts;

public static class ClassifiedAds
{
    public static class V1
    {

        public class Create : IClassifiedAdCommand
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
        }

        public record SetTitle(Guid Id, string Title);

        public record UpdateText(Guid Id, string Text);

        public record UpdatePrice(Guid Id, decimal Price, string Currency);

        public record RequestToPublish(Guid Id);
        public interface IClassifiedAdCommand
        {
            public Guid Id { get; set; }
        }
        
    }
}