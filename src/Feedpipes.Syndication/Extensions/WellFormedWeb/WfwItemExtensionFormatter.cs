using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.WellFormedWeb.Entities;
using Feedpipes.Syndication.Xml;

namespace Feedpipes.Syndication.Extensions.WellFormedWeb
{
    internal class WfwItemExtensionFormatter
    {
        public static bool TryFormatWfwItemExtension(WfwItemExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
        {
            elements = default;

            if (extensionToFormat == null)
                return false;

            elements = new List<XElement>();

            if (TryFormatWfwTextElement(extensionToFormat.Comment, "comment", namespaceAliases, out var commentElement))
            {
                elements.Add(commentElement);
            }

            if (TryFormatWfwTextElement(extensionToFormat.CommentRss, "commentRss", namespaceAliases, out var commentRssElement))
            {
                elements.Add(commentRssElement);
            }

            if (!elements.Any())
                return false;

            return true;
        }

        private static bool TryFormatWfwTextElement(string valueToFormat, string elementName, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (string.IsNullOrWhiteSpace(valueToFormat))
                return false;

            namespaceAliases.EnsureNamespaceAlias(WfwConstants.NamespaceAlias, WfwConstants.Namespace);
            element = new XElement(WfwConstants.Namespace + elementName) { Value = valueToFormat };
            return true;
        }
    }
}