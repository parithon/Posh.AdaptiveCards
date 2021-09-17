using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Posh.AdaptiveCards.Models
{
    public class TeamsMessage
    {
        public string Type { get; } = "message";
        public IList<TeamsAdaptiveCard> Attachments { get; } = new Collection<TeamsAdaptiveCard>();
    }
}
