using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Content.Entities;
using Feedpipes.Syndication.Xml;

namespace Feedpipes.Syndication.Extensions.Rss10Content
{
    internal static class Rss10ContentItemExtensionFormatter
    {
        public static bool TryFormatRss10ContentItemExtension(Rss10ContentItemExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
        {
            elements = default;

            if (extensionToFormat == null)
                return false;

            elements = new List<XElement>();

            if (TryFormatRss10ContentEncoded(extensionToFormat.Encoded, namespaceAliases, out var encodedElement))
            {
                elements.Add(encodedElement);
            }

            if (!elements.Any())
                return false;

            return true;
        }

        private static bool TryFormatRss10ContentEncoded(string valueToFormat, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (string.IsNullOrWhiteSpace(valueToFormat))
                return false;

            namespaceAliases.EnsureNamespaceAlias(Rss10ContentConstants.NamespaceAlias, Rss10ContentConstants.Namespace);
            element = new XElement(Rss10ContentConstants.Namespace + "encoded");
            element.Add(new XCData(valueToFormat));

            return true;
        }
    }
}