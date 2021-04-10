using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Feedpipes.Extensions.DublinCore.Entities;
using Feedpipes.Timestamps.Relaxed;

namespace Feedpipes.Extensions.DublinCore
{
    internal static class DublinCoreExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseDublinCoreExtension(XElement parentElement, out DublinCoreExtension extension)
        {
            extension = null;

            if (parentElement == null)
                return false;

            foreach (var ns in DublinCoreExtensionConstants.RecognizedNamespaces)
            {
                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "title"), out var parsedTitle))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Title = parsedTitle;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "creator"), out var parsedCreator))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Creator = parsedCreator;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "subject"), out var parsedSubject))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Subject = parsedSubject;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "description"), out var parsedDescription))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Description = parsedDescription;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "publisher"), out var parsedPublisher))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Publisher = parsedPublisher;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "contributor"), out var parsedContributor))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Contributor = parsedContributor;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "type"), out var parsedType))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Type = parsedType;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "format"), out var parsedFormat))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Format = parsedFormat;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "identifier"), out var parsedIdentifier))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Identifier = parsedIdentifier;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "source"), out var parsedSource))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Source = parsedSource;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "language"), out var parsedLanguage))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Language = parsedLanguage;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "relation"), out var parsedRelation))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Relation = parsedRelation;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "coverage"), out var parsedCoverage))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Coverage = parsedCoverage;
                }

                if (TryParseDublinCoreTextElement(parentElement.Element(ns + "rights"), out var parsedRights))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Rights = parsedRights;
                }

                if (TryParseDublinCoreTimestamp(parentElement.Element(ns + "date"), out var parsedDate))
                {
                    extension = extension ?? new DublinCoreExtension();
                    extension.Date = parsedDate;
                }

                if (TryParseDublinCoreTimestamp(parentElement.Element(ns + "modified"), out var parsedModified))
                {
                    extension = extension ?? new DublinCoreExtension();
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
