using AdaptiveCards;

namespace Posh.AdaptiveCards.Models
{
    public class TeamsAdaptiveCard
    {
        public TeamsAdaptiveCard()
        {
        }

        public TeamsAdaptiveCard(AdaptiveCard card)
        {
            Content = card;
        }

        public string ContentType { get; } = AttachmentContentTypes.AdaptiveCard;
        public string ContentUrl { get; } = string.Empty;
        public AdaptiveCard Content { get; set; }
    }
}
