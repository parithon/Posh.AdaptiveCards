using AdaptiveCards;

namespace Posh.AdaptiveCards.Models
{
    public class TeamsAdaptiveCard
    {
        public string ContentType { get; } = AttachmentContentTypes.AdaptiveCard;
        public string ContentUrl { get; } = string.Empty;
        public AdaptiveCard Content { get; set; }
    }
}
