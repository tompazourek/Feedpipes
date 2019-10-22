using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Extensions.WellFormedWeb.Entities;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Extensions.WellFormedWeb
{
    internal class WellFormedWebExtensionFormatter
    {
        public static bool TryFormatWellFormedWebExtension(WellFormedWebExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
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

            namespaceAliases.EnsureNamespaceAlias(WellFormedWebExtensionConstants.NamespaceAlias, WellFormedWebExtensionConstants.Namespace);
            element = new XElement(WellFormedWebExtensionConstants.Namespace + elementName) { Value = valueToFormat };
            return true;
        }
    }
}