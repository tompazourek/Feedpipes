using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Extensions.Rss10Slash.Entities;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Extensions.Rss10Slash
{
    internal static class Rss10SlashExtensionFormatter
    {
        public static bool TryFormatRss10SlashExtension(Rss10SlashExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
        {
            elements = default;

            if (extensionToFormat == null)
                return false;

            elements = new List<XElement>();

            if (TryFormatRss10SlashTextElement(extensionToFormat.Section, "section", namespaceAliases, out var sectionElement))
            {
                elements.Add(sectionElement);
            }

            if (TryFormatRss10SlashTextElement(extensionToFormat.Department, "department", namespaceAliases, out var departmentElement))
            {
                elements.Add(departmentElement);
            }

            if (TryFormatRss10SlashComments(extensionToFormat.Comments, namespaceAliases, out var commentsElement))
            {
                elements.Add(commentsElement);
            }

            if (TryFormatRss10SlashHitParade(extensionToFormat.HitParade, namespaceAliases, out var hitParadeElement))
            {
                elements.Add(hitParadeElement);
            }

            if (!elements.Any())
                return false;

            return true;
        }

        private static bool TryFormatRss10SlashTextElement(string valueToFormat, string elementName, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (string.IsNullOrWhiteSpace(valueToFormat))
                return false;

            namespaceAliases.EnsureNamespaceAlias(Rss10SlashExtensionConstants.NamespaceAlias, Rss10SlashExtensionConstants.Namespace);
            element = new XElement(Rss10SlashExtensionConstants.Namespace + elementName) { Value = valueToFormat };
            return true;
        }

        private static bool TryFormatRss10SlashComments(int? valueToFormat, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (valueToFormat == null)
                return false;

            var valueString = valueToFormat.Value.ToString(CultureInfo.InvariantCulture);
            namespaceAliases.EnsureNamespaceAlias(Rss10SlashExtensionConstants.NamespaceAlias, Rss10SlashExtensionConstants.Namespace);
            element = new XElement(Rss10SlashExtensionConstants.Namespace + "comments") { Value = valueString };

            return true;
        }

        private static bool TryFormatRss10SlashHitParade(IList<int> valueToFormat, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (valueToFormat?.Any() != true)
                return false;

            var valueString = string.Join(",", valueToFormat.Select(x => x.ToString(CultureInfo.InvariantCulture)));
            namespaceAliases.EnsureNamespaceAlias(Rss10SlashExtensionConstants.NamespaceAlias, Rss10SlashExtensionConstants.Namespace);
            element = new XElement(Rss10SlashExtensionConstants.Namespace + "hit_parade") { Value = valueString };

            return true;
        }
    }
}
