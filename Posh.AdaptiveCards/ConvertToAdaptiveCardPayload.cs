using Posh.AdaptiveCards.Helpers;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace Posh.AdaptiveCards
{
    [Cmdlet(VerbsData.ConvertTo, "AdaptiveCardPayload", DefaultParameterSetName = "Template")]
    public class ConvertToAdaptiveCardPayload : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public PSObject InputObject { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Template")]
        public string Template { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Path")]
        public FileInfo TemplatePath { get; set; }

        private string _templateJson;

        protected override void BeginProcessing()
        {
            if (this.ParameterSetName == "Template")
            {
                _templateJson = Template;
            }
            else if (ParameterSetName == "Path")
            {
                var originalPath = this.GetResolvedProviderPathFromPSPath(TemplatePath.ToString(), out ProviderInfo _).FirstOrDefault();
                _templateJson = File.ReadAllText(originalPath, Encoding.UTF8);
            }
        }

        protected override void ProcessRecord()
        {
            var (cardPayload, data) = AdaptiveCardProcessor.ProcessDataToCardPayload(_templateJson, InputObject);
            WriteVerbose($"Data used for the template:\n{data}");
            WriteObject(cardPayload);
        }
    }
}
