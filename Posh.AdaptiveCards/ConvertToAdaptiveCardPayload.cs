using Posh.AdaptiveCards.Module.Helpers;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace Posh.AdaptiveCards.Module
{
    [Cmdlet(VerbsData.ConvertTo, "AdaptiveCardPayload", DefaultParameterSetName = "Template")]
    public class ConvertToAdaptiveCardPayload : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public PSObject InputObject { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Path")]
        public FileInfo TemplatePath { get; set; }

        [Parameter(Position = 1, ParameterSetName = "Template")]
        public string Template { get; set; }

        private string _templateJson;

        protected override void BeginProcessing()
        {
            if (ParameterSetName == "Template")
            {
                if (string.IsNullOrWhiteSpace(Template))
                {
                    PathInfo pi = this.SessionState.Path.CurrentFileSystemLocation;
                    DirectoryInfo di = new DirectoryInfo(pi.Path);
                    FileInfo fi = di.GetFiles("*.json").FirstOrDefault(f => f.Name.Contains("adaptive-card") || f.Name.Contains("template"));
                    if (null == fi)
                    {
                        throw new ParameterBindingException("Either Template or TemplatePath must be provided unless a template.json file exists in the location this cmdlet was called.");
                    }
                    _templateJson = File.ReadAllText(fi.FullName, Encoding.UTF8);
                }
            }
            else if (ParameterSetName == "Path")
            {
                _templateJson = File.ReadAllText(TemplatePath.FullName, Encoding.UTF8);
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
