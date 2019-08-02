using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.DublinCore.Entities;
using Feedpipes.Syndication.Timestamps.Relaxed;

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

            foreach (var ns in DublinCoreConstants.RecognizedNamespaces)
            {
                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "title"), out var parsedTitle))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Title = parsedTitle;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "creator"), out var parsedCreator))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Creator = parsedCreator;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "subject"), out var parsedSubject))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Subject = parsedSubject;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "description"), out var parsedDescription))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Description = parsedDescription;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "publisher"), out var parsedPublisher))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Publisher = parsedPublisher;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "contributor"), out var parsedContributor))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Contributor = parsedContributor;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "type"), out var parsedType))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Type = parsedType;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "format"), out var parsedFormat))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Format = parsedFormat;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "identifier"), out var parsedIdentifier))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Identifier = parsedIdentifier;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "source"), out var parsedSource))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Source = parsedSource;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "language"), out var parsedLanguage))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Language = parsedLanguage;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "relation"), out var parsedRelation))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Relation = parsedRelation;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "coverage"), out var parsedCoverage))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Coverage = parsedCoverage;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "rights"), out var parsedRights))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Rights = parsedRights;
                }

                if (TryParseDublinCoreTimestamp(parentElement.Element(ns + "date"), out var parsedDate))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Date = parsedDate;
                }

                if (TryParseDublinCoreTimestamp(parentElement.Element(ns + "modified"), out var parsedModified))
                {
                    extension = extension ?? new DublinCoreElementExtension();
                    extension.Modified = parsedModified;
                }
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

        private static bool TryParseDublinCoreTimestamp(XElement element, out DateTimeOffset parsedValue)
        {
            parsedValue = default;

            if (element == null)
                return false;

            if (!RelaxedTimestampParser.TryParseTimestampFromString(element.Value, out parsedValue))
                return false;

            return true;
        }
    }
}