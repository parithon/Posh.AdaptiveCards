using NUnit.Framework;
using System.Management.Automation;

namespace Posh.AdaptiveCards.Tests
{
    public class ConvertToAdaptiveCardPayloadTests
    {
        [Test]
        public void InputObject_HasRequiredAttributes()
        {
            Assert.IsTrue(
                TypeHelpers.CmdletParameterHasAttribute(
                    typeof(ConvertToAdaptiveCardPayload),
                    nameof(ConvertToAdaptiveCardPayload.InputObject),
                    typeof(ParameterAttribute)
                    )
                );
            Assert.IsTrue(
                TypeHelpers.CmdletParameterPropertyIsDefined(
                    typeof(ConvertToAdaptiveCardPayload),
                    nameof(ConvertToAdaptiveCardPayload.InputObject),
                    "Mandatory",
                    true
                    )
                );
            Assert.IsTrue(
                TypeHelpers.CmdletParameterPropertyIsDefined(
                    typeof(ConvertToAdaptiveCardPayload),
                    nameof(ConvertToAdaptiveCardPayload.InputObject),
                    "ValueFromPipeline",
                    true
                    )
                );
        }

        [Test]
        public void TemplatePath_HasRequiredAttributes()
        {
            Assert.IsTrue(
                TypeHelpers.CmdletParameterHasAttribute(
                    typeof(ConvertToAdaptiveCardPayload),
                    nameof(ConvertToAdaptiveCardPayload.TemplatePath),
                    typeof(ParameterAttribute)
                    )
                );
            Assert.IsTrue(
                TypeHelpers.CmdletParameterPropertyIsDefined(
                    typeof(ConvertToAdaptiveCardPayload),
                    nameof(ConvertToAdaptiveCardPayload.TemplatePath),
                    "Mandatory",
                    true
                    )
                );
            Assert.IsTrue(
                TypeHelpers.CmdletParameterPropertyIsDefined(
                    typeof(ConvertToAdaptiveCardPayload),
                    nameof(ConvertToAdaptiveCardPayload.TemplatePath),
                    "ParameterSetName",
                    "Path"
                    )
                );
        }

        [Test]
        public void Template_HasRequiredAttributes()
        {
            Assert.IsTrue(
                TypeHelpers.CmdletParameterHasAttribute(
                    typeof(ConvertToAdaptiveCardPayload),
                    nameof(ConvertToAdaptiveCardPayload.Template),
                    typeof(ParameterAttribute)
                    )
                );
            Assert.IsTrue(
                TypeHelpers.CmdletParameterPropertyIsDefined(
                    typeof(ConvertToAdaptiveCardPayload),
                    nameof(ConvertToAdaptiveCardPayload.Template),
                    "Mandatory",
                    true
                    )
                );
            Assert.IsTrue(
                TypeHelpers.CmdletParameterPropertyIsDefined(
                    typeof(ConvertToAdaptiveCardPayload),
                    nameof(ConvertToAdaptiveCardPayload.Template),
                    "ParameterSetName",
                    "Template"
                    )
                );
        }
    }
}
