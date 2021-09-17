using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Posh.AdaptiveCards.Models;
using Posh.AdaptiveCards.Helpers;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Text;

namespace Posh.AdaptiveCards
{
    [Cmdlet(VerbsCommunications.Send, "AdaptiveCard")]
    public class SendAdaptiveCard : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string Payload { get; set; }

        [Parameter(Mandatory = true)]
        public Uri WebhookUrl { get; set; }

        [Parameter()]
        public SwitchParameter PassThru { get; set; }

        private HttpClient _client;
        
        protected override void BeginProcessing()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Posh.AdaptiveCard");
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            _client.DefaultRequestHeaders.TryAddWithoutValidation("AcceptEncoding", "UTF8");
        }

        protected override void ProcessRecord()
        {
            var (card, warnings) = AdaptiveCardProcessor.ProcessDataToAdaptiveCard(Payload);
            foreach (var warning in warnings)
            {
                WriteWarning(warning.Message);
            }

            TeamsMessage message = new TeamsMessage();
            message.Attachments.Add(new TeamsAdaptiveCard(card));

            string payload = JsonConvert.SerializeObject(message, new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            });

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, WebhookUrl)
            {
                Content = new StringContent(payload, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = _client.SendAsync(request).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            if (PassThru.IsPresent)
            {
                WriteObject(response.Content);
            }
        }

        protected override void EndProcessing()
        {
            _client.CancelPendingRequests();
            _client.Dispose();
        }
    }
}
