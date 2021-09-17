using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Posh.AdaptiveCards.Tests
{
    public static class TypeHelpers
    {
        public static bool CmdletParameterHasAttribute(Type cmdletType, string propertyName, Type attribute)
        {
            var property = cmdletType.GetProperty(propertyName);
            return Attribute.IsDefined(property, attribute);
        }

        public static bool CmdletParameterPropertyIsDefined<T>(Type cmdletType, string propertyName, string attributePropertyName, T value)
        {
            var cmdletProperty = cmdletType.GetProperty(propertyName);
            var parameterAttributes = cmdletProperty.CustomAttributes.Where(attrib => attrib.AttributeType == typeof(ParameterAttribute));
            return parameterAttributes.Any(attrib => attrib.NamedArguments.Any(arg => arg.MemberName == attributePropertyName && Equals(arg.TypedValue.Value, value)));
        }
    }
}
