﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml.Linq;
using Feedpipes.Syndication.Atom10.Entities;
using Feedpipes.Syndication.Extensions.RssAtom10.Entities;
using Feedpipes.Syndication.Timestamps.Relaxed;

namespace Feedpipes.Syndication.Extensions.RssAtom10
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    internal static class RssAtom10ElementExtensionParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        public static bool TryParseRssAtom10ElementExtension(XElement parentElement, out RssAtom10ElementExtension extension)
        {
            extension = null;

            if (parentElement == null)
                return false;

            foreach (var ns in RssAtom10Constants.RecognizedNamespaces)
            {
                if (TryParseRssAtom10TextElement(parentElement.Element(ns + "id"), out var parsedId))
                {
                    extension = extension ?? new RssAtom10ElementExtension();
                    extension.Id = parsedId;
                }

                if (TryParseRssAtom10Timestamp(parentElement.Element(ns + "updated"), out var parsedUpdated))
                {
                    extension = extension ?? new RssAtom10ElementExtension();
                    extension.Updated = parsedUpdated;
                }

                foreach (var linkElement in parentElement.Elements(ns + "link"))
                {
                    if (TryParseRssAtom10Link(linkElement, out var parsedLink))
                    {
                        extension = extension ?? new RssAtom10ElementExtension();
                        extension.Links.Add(parsedLink);
                    }
                }
            }

            return extension != null;
        }

        private static bool TryParseRssAtom10TextElement(XElement textElement, out string parsedText)
        {
            parsedText = default;

            if (string.IsNullOrEmpty(textElement?.Value))
                return false;

            parsedText = textElement.Value.Trim();
            return true;
        }

        private static bool TryParseRssAtom10Timestamp(XElement timestampElement, out DateTimeOffset parsedTimestamp)
        {
            parsedTimestamp = default;

            if (timestampElement == null)
                return false;

            if (!RelaxedTimestampParser.TryParseTimestampFromString(timestampElement.Value, out parsedTimestamp))
                return false;

            return true;
        }

        private static bool TryParseRssAtom10Link(XElement linkElement, out Atom10Link parsedLink)
        {
            parsedLink = default;

            if (linkElement == null)
                return false;

            parsedLink = new Atom10Link();

            parsedLink.Href = linkElement.Attribute("href")?.Value;
            parsedLink.Hreflang = linkElement.Attribute("hreflang")?.Value;

            parsedLink.Rel = linkElement.Attribute("rel")?.Value ?? "alternate";
            parsedLink.Title = linkElement.Attribute("title")?.Value;
            parsedLink.Type = linkElement.Attribute("type")?.Value;

            if (int.TryParse(linkElement.Attribute("length")?.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedLength))
            {
                parsedLink.Length = parsedLength;
            }

            return true;
        }
    }
}