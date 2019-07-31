using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.Rss10Content.Entities;
using Feedpipes.Syndication.Extensions.Rss10Slash;

namespace Feedpipes.Syndication.Extensions.DublinCore
{
    internal static class DublinCoreElementExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseDublinCoreElementExtension(XElement parentElement, out DublinCoreElementExtension extension)
        {
            extension = null;

            if (parentElement == null)
                return false;

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "title"), out var parsedTitle))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Title = parsedTitle;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "creator"), out var parsedCreator))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Creator = parsedCreator;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "subject"), out var parsedSubject))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Subject = parsedSubject;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "description"), out var parsedDescription))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Description = parsedDescription;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "publisher"), out var parsedPublisher))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Publisher = parsedPublisher;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "contributor"), out var parsedContributor))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Contributor = parsedContributor;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "type"), out var parsedType))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Type = parsedType;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "format"), out var parsedFormat))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Format = parsedFormat;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "identifier"), out var parsedIdentifier))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Identifier = parsedIdentifier;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "source"), out var parsedSource))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Source = parsedSource;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "language"), out var parsedLanguage))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Language = parsedLanguage;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "relation"), out var parsedRelation))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Relation = parsedRelation;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "coverage"), out var parsedCoverage))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Coverage = parsedCoverage;
            }

            if (TryParseDublinCoreTextElement(parentElement.Element(DublinCoreConstants.Namespace + "rights"), out var parsedRights))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Rights = parsedRights;
            }


            if (TryParseDublinCoreDate(parentElement.Element(DublinCoreConstants.Namespace + "date"), out var parsedDate))
            {
                extension = extension ?? new DublinCoreElementExtension();
                extension.Date = parsedDate;
            }

            return extension != null;
        }

        private static bool TryParseDublinCoreTextElement(XElement element, out string parsedValue)
        {
            parsedValue = default;

            if (element == null)
                return false;

            parsedValue = element.Value;
            return true;
        }

        private static bool TryParseDublinCoreDate(XElement element, out DateTimeOffset parsedValue)
        {
            parsedValue = default;

            if (element == null)
                return false;

            var valueString = element.Value.Trim().ToUpperInvariant();
            var formatProvider = CultureInfo.InvariantCulture;

            return DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mmzzz", formatProvider, DateTimeStyles.None, out parsedValue)
                   || DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mm", formatProvider, DateTimeStyles.AdjustToUniversal, out parsedValue)
                   || DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mm'Z'", formatProvider, DateTimeStyles.AdjustToUniversal, out parsedValue)
                   || DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mm':'sszzz", formatProvider, DateTimeStyles.None, out parsedValue)
                   || DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mm':'ss", formatProvider, DateTimeStyles.AdjustToUniversal, out parsedValue)
                   || DateTimeOffset.TryParseExact(valueString, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", formatProvider, DateTimeStyles.AdjustToUniversal, out parsedValue);
        }
    }
}