using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Extensions.CreativeCommons.Entities;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Extensions.CreativeCommons
{
    internal static class CreativeCommonsExtensionFormatter
    {
        public static bool TryFormatCreativeCommonsExtension(CreativeCommonsExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
        {
            elements = default;

            if (extensionToFormat == null)
                return false;

            elements = new List<XElement>();

            foreach (var licenseToFormat in extensionToFormat.Licenses)
            {
                if (TryFormatCreativeCommonsTextElement(licenseToFormat, namespaceAliases, out var licenseElement))
                {
                    elements.Add(licenseElement);
                }
            }

            if (!elements.Any())
                return false;

            return true;
        }

        private static bool TryFormatCreativeCommonsTextElement(CreativeCommonsLicense licenseToFormat, XNamespaceAliasSet namespaceAliases, out XElement licenseElement)
        {
            licenseElement = default;

            if (string.IsNullOrWhiteSpace(licenseToFormat?.Value))
                return false;

            namespaceAliases.EnsureNamespaceAlias(CreativeCommonsExtensionConstants.NamespaceAlias, CreativeCommonsExtensionConstants.Namespace);
            licenseElement = new XElement(CreativeCommonsExtensionConstants.Namespace + "license") { Value = licenseToFormat.Value };
            return true;
        }
    }
}
