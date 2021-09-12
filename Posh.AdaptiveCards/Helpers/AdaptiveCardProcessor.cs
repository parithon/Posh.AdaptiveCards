using AdaptiveCards;
using AdaptiveCards.Templating;
using System.Collections.Generic;
using System.Management.Automation;

namespace Posh.AdaptiveCards.Module.Helpers
{
    public struct AdaptiveCardProcessor
    {
        public static (string payload, string data) ProcessDataToCardPayload(string templateJson, PSObject inputObject)
        {
            AdaptiveCardTemplate template = new AdaptiveCardTemplate(templateJson);
            string data = ConvertToJson(inputObject);
            string json = template.Expand(data);
            return (json, data);
        }

        public static (AdaptiveCard card, IList<AdaptiveWarning> warnings) ProcessDataToAdaptiveCard(string cardPayload)
        {
            var result = AdaptiveCard.FromJson(cardPayload);
            return (result.Card, result.Warnings);
        }

        public static (AdaptiveCard card, IList<AdaptiveWarning> warnings) ProcessDataToAdaptiveCard(string templateJson, PSObject inputObject)
        {
            var (json, _) = ProcessDataToCardPayload(templateJson, inputObject);
            return ProcessDataToAdaptiveCard(json);
        }

        private static string ConvertToJson(PSObject inputObject)
        {
            if (inputObject.TypeNames.Contains("System.String")) return inputObject.BaseObject.ToString();
            using PowerShell ps = PowerShell.Create();
            ps.AddCommand("ConvertTo-Json");
            ps.AddParameter("Depth", "99");
            ps.AddParameter("InputObject", inputObject);
            inputObject = ps.Invoke()[0];
            return inputObject.ToString();
        }
    }
}
