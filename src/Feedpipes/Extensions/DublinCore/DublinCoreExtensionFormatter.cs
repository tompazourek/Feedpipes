﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Extensions.DublinCore.Entities;
using Feedpipes.Timestamps.Rfc3339;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Extensions.DublinCore
{
    internal static class DublinCoreExtensionFormatter
    {
        public static bool TryFormatDublinCoreExtension(DublinCoreExtension extensionToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
        {
            elements = default;

            if (extensionToFormat == null)
                return false;

            elements = new List<XElement>();

            if (TryFormatDublinCoreTextElement(extensionToFormat.Title, "title", namespaceAliases, out var titleElement))
            {
                elements.Add(titleElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Creator, "creator", namespaceAliases, out var creatorElement))
            {
                elements.Add(creatorElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Subject, "subject", namespaceAliases, out var subjectElement))
            {
                elements.Add(subjectElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Description, "description", namespaceAliases, out var descriptionElement))
            {
                elements.Add(descriptionElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Publisher, "publisher", namespaceAliases, out var publisherElement))
            {
                elements.Add(publisherElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Contributor, "contributor", namespaceAliases, out var contributorElement))
            {
                elements.Add(contributorElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Type, "type", namespaceAliases, out var typeElement))
            {
                elements.Add(typeElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Format, "format", namespaceAliases, out var formatElement))
            {
                elements.Add(formatElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Identifier, "identifier", namespaceAliases, out var identifierElement))
            {
                elements.Add(identifierElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Source, "source", namespaceAliases, out var sourceElement))
            {
                elements.Add(sourceElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Language, "language", namespaceAliases, out var languageElement))
            {
                elements.Add(languageElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Relation, "relation", namespaceAliases, out var relationElement))
            {
                elements.Add(relationElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Coverage, "coverage", namespaceAliases, out var coverageElement))
            {
                elements.Add(coverageElement);
            }

            if (TryFormatDublinCoreTextElement(extensionToFormat.Rights, "rights", namespaceAliases, out var rightsElement))
            {
                elements.Add(rightsElement);
            }

            if (TryFormatDublinCoreTimestamp(extensionToFormat.Date, "date", namespaceAliases, out var dateElement))
            {
                elements.Add(dateElement);
            }

            if (TryFormatDublinCoreTimestamp(extensionToFormat.Modified, "modified", namespaceAliases, out var modifiedElement))
            {
                elements.Add(modifiedElement);
            }

            if (!elements.Any())
                return false;

            return true;
        }

        private static bool TryFormatDublinCoreTextElement(string valueToFormat, string elementName, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (string.IsNullOrWhiteSpace(valueToFormat))
                return false;

            namespaceAliases.EnsureNamespaceAlias(DublinCoreExtensionConstants.NamespaceAlias, DublinCoreExtensionConstants.Namespace);
            element = new XElement(DublinCoreExtensionConstants.Namespace + elementName) { Value = valueToFormat };
            return true;
        }

        private static bool TryFormatDublinCoreTimestamp(DateTimeOffset? valueToFormat, string elementName, XNamespaceAliasSet namespaceAliases, out XElement element)
        {
            element = default;

            if (valueToFormat == null)
                return false;

            if (!Rfc3339TimestampFormatter.TryFormatTimestampAsString(valueToFormat.Value, out var valueString))
                return false;

            namespaceAliases.EnsureNamespaceAlias(DublinCoreExtensionConstants.NamespaceAlias, DublinCoreExtensionConstants.Namespace);
            element = new XElement(DublinCoreExtensionConstants.Namespace + elementName) { Value = valueString };

            return true;
        }
    }
}
