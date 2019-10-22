using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Atom10.Entities;
using Feedpipes.Extensions.RssAtom10.Entities;
using Feedpipes.Timestamps.Rfc3339;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Extensions.RssAtom10
{
    internal static class RssAtom10ExtensionFormatter
    {
        public static bool TryFormatRssAtom10Extension(RssAtom10Extension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
        {
            elements = default;

            if (extensionToFormat == null)
                return false;

            elements = new List<XElement>();

            if (TryFormatRssAtom10TextElement(extensionToFormat.Id, "id", namespaceAliases, out var idElement))
            {
                elements.Add(idElement);
            }

            if (TryFormatRssAtom10Timestamp(extensionToFormat.Updated, "updated", namespaceAliases, out var updatedElement))
            {
                elements.Add(updatedElement);
            }

            foreach (var linkToFormat in extensionToFormat.Links ?? Enumerable.Empty<Atom10Link>())
            {
                if (TryFormatRssAtom10Link(linkToFormat, namespaceAliases, out var linkElement))
                {
                    elements.Add(linkElement);
                }
            }

            if (!elements.Any())
                return false;

            return true;
        }

        private static bool TryFormatRssAtom10TextElement(string valueToFormat, string elementName, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (string.IsNullOrWhiteSpace(valueToFormat))
                return false;

            namespaceAliases.EnsureNamespaceAlias(RssAtom10ExtensionConstants.NamespaceAlias, RssAtom10ExtensionConstants.Namespace);
            element = new XElement(RssAtom10ExtensionConstants.Namespace + elementName) { Value = valueToFormat };
            return true;
        }

        private static bool TryFormatRssAtom10Timestamp(DateTimeOffset? valueToFormat, string elementName, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (valueToFormat == null)
                return false;

            if (!Rfc3339TimestampFormatter.TryFormatTimestampAsString(valueToFormat.Value, out var valueString))
                return false;

            namespaceAliases.EnsureNamespaceAlias(RssAtom10ExtensionConstants.NamespaceAlias, RssAtom10ExtensionConstants.Namespace);
            element = new XElement(RssAtom10ExtensionConstants.Namespace + elementName) { Value = valueString };

            return true;
        }

        private static bool TryFormatRssAtom10Link(Atom10Link linkToFormat, XNamespaceAliasSet namespaceAliases, out XElement linkElement)
        {
            linkElement = default;

            if (string.IsNullOrEmpty(linkToFormat.Href))
                return false;

            namespaceAliases.EnsureNamespaceAlias(RssAtom10ExtensionConstants.NamespaceAlias, RssAtom10ExtensionConstants.Namespace);
            linkElement = new XElement(RssAtom10ExtensionConstants.Namespace + "link");

            linkElement.Add(new XAttribute("href", linkToFormat.Href));

            if (TryFormatRssAtom10OptionalTextAttribute(linkToFormat.Rel, "rel", out var relAttribute))
            {
                linkElement.Add(relAttribute);
            }

            if (TryFormatRssAtom10OptionalTextAttribute(linkToFormat.Type, "type", out var typeAttribute))
            {
                linkElement.Add(typeAttribute);
            }

            if (TryFormatRssAtom10OptionalTextAttribute(linkToFormat.Hreflang, "hreflang", out var hreflangAttribute))
            {
                linkElement.Add(hreflangAttribute);
            }

            if (TryFormatRssAtom10OptionalTextAttribute(linkToFormat.Title, "title", out var titleAttribute))
            {
                linkElement.Add(titleAttribute);
            }

            if (TryFormatRssAtom10OptionalNumericAttribute(linkToFormat.Length, "length", out var lengthAttribute))
            {
                linkElement.Add(lengthAttribute);
            }

            return true;
        }

        private static bool TryFormatRssAtom10OptionalTextAttribute(string valueToFormat, XName name, out XAttribute attribute)
        {
            attribute = default;

            if (string.IsNullOrWhiteSpace(valueToFormat))
                return false;

            attribute = new XAttribute(name, valueToFormat);
            return true;
        }

        private static bool TryFormatRssAtom10OptionalNumericAttribute(int? valueToFormat, XName name, out XAttribute attribute)
        {
            attribute = default;

            if (valueToFormat == null)
                return false;

            var valueString = valueToFormat.Value.ToString(CultureInfo.InvariantCulture);
            attribute = new XAttribute(name, valueString);
            return true;
        }
    }
}