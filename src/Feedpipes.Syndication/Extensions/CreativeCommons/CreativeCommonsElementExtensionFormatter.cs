using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.CreativeCommons.Entities;
using Feedpipes.Syndication.Utils.Xml;

namespace Feedpipes.Syndication.Extensions.CreativeCommons
{
    internal static class CreativeCommonsElementExtensionFormatter
    {
        public static bool TryFormatCreativeCommonsElementExtension(CreativeCommonsElementExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
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

            namespaceAliases.EnsureNamespaceAlias(CreativeCommonsConstants.NamespaceAlias, CreativeCommonsConstants.Namespace);
            licenseElement = new XElement(CreativeCommonsConstants.Namespace + "license") { Value = licenseToFormat.Value };
            return true;
        }
    }
}