using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Xml.Linq;
using Feedpipes.Syndication.Atom10.Entities;
using Feedpipes.Syndication.Rfc3339Timestamp;

namespace Feedpipes.Syndication.Atom10
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class Atom10FeedParser
    {
        private static readonly XNamespace _atom = Atom10Constants.Atom10Namespace;
        private static readonly XNamespace _xml = XNamespace.Xml;

        public static bool TryParseAtom10Feed(XDocument document, out Atom10Feed parsedFeed)
        {
            parsedFeed = default;

            var feedElement = document?.Element(_atom + "feed");
            if (feedElement == null)
                return false;

            parsedFeed = new Atom10Feed();

            parsedFeed.Lang = feedElement.Attribute(_xml + "lang")?.Value;
            parsedFeed.Base = feedElement.Attribute(_xml + "base")?.Value;

            parsedFeed.Id = feedElement.Element(_atom + "id")?.Value.Trim();

            if (TryParseAtom10Text(feedElement.Element(_atom + "title"), out var parsedTitle))
            {
                parsedFeed.Title = parsedTitle;
            }

            if (TryParseAtom10Timestamp(feedElement.Element(_atom + "updated"), out var parsedUpdated))
            {
                parsedFeed.Updated = parsedUpdated;
            }

            foreach (var authorElement in feedElement.Elements(_atom + "author"))
            {
                if (TryParseAtom10Person(authorElement, out var parsedPerson))
                {
                    parsedFeed.Authors.Add(parsedPerson);
                }
            }

            foreach (var linkElement in feedElement.Elements(_atom + "link"))
            {
                if (TryParseAtom10Link(linkElement, out var parsedLink))
                {
                    parsedFeed.Links.Add(parsedLink);
                }
            }

            foreach (var categoryElement in feedElement.Elements(_atom + "category"))
            {
                if (TryParseAtom10Category(categoryElement, out var parsedCategory))
                {
                    parsedFeed.Categories.Add(parsedCategory);
                }
            }

            foreach (var contributorElement in feedElement.Elements(_atom + "contributor"))
            {
                if (TryParseAtom10Person(contributorElement, out var parsedPerson))
                {
                    parsedFeed.Contributors.Add(parsedPerson);
                }
            }

            if (TryParseAtom10Generator(feedElement.Element(_atom + "generator"), out var parsedGenerator))
            {
                parsedFeed.Generator = parsedGenerator;
            }

            parsedFeed.Icon = feedElement.Element(_atom + "icon")?.Value.Trim();
            parsedFeed.Logo = feedElement.Element(_atom + "logo")?.Value.Trim();

            if (TryParseAtom10Text(feedElement.Element(_atom + "rights"), out var parsedRights))
            {
                parsedFeed.Rights = parsedRights;
            }

            if (TryParseAtom10Text(feedElement.Element(_atom + "subtitle"), out var parsedSubtitle))
            {
                parsedFeed.Subtitle = parsedSubtitle;
            }

            // entries
            foreach (var entryElement in feedElement.Elements(_atom + "entry"))
            {
                if (TryParseAtom10Entry(entryElement, out var parsedEntry))
                {
                    parsedFeed.Entries.Add(parsedEntry);
                }
            }

            return true;
        }

        private static bool TryParseAtom10Entry(XElement entryElement, out Atom10Entry parsedEntry)
        {
            parsedEntry = default;

            if (entryElement == null)
                return false;

            parsedEntry = new Atom10Entry();

            parsedEntry.Id = entryElement.Element(_atom + "id")?.Value.Trim();

            if (TryParseAtom10Text(entryElement.Element(_atom + "title"), out var parsedTitle))
            {
                parsedEntry.Title = parsedTitle;
            }

            if (TryParseAtom10Timestamp(entryElement.Element(_atom + "updated"), out var parsedUpdated))
            {
                parsedEntry.Updated = parsedUpdated;
            }

            foreach (var authorElement in entryElement.Elements(_atom + "author"))
            {
                if (TryParseAtom10Person(authorElement, out var parsedPerson))
                {
                    parsedEntry.Authors.Add(parsedPerson);
                }
            }

            if (TryParseAtom10Content(entryElement.Element(_atom + "content"), out var parsedContent))
            {
                parsedEntry.Content = parsedContent;
            }

            foreach (var linkElement in entryElement.Elements(_atom + "link"))
            {
                if (TryParseAtom10Link(linkElement, out var parsedLink))
                {
                    parsedEntry.Links.Add(parsedLink);
                }
            }

            if (TryParseAtom10Text(entryElement.Element(_atom + "summary"), out var parsedSummary))
            {
                parsedEntry.Summary = parsedSummary;
            }

            foreach (var categoryElement in entryElement.Elements(_atom + "category"))
            {
                if (TryParseAtom10Category(categoryElement, out var parsedCategory))
                {
                    parsedEntry.Categories.Add(parsedCategory);
                }
            }

            foreach (var contributorElement in entryElement.Elements(_atom + "contributor"))
            {
                if (TryParseAtom10Person(contributorElement, out var parsedPerson))
                {
                    parsedEntry.Contributors.Add(parsedPerson);
                }
            }

            if (TryParseAtom10Timestamp(entryElement.Element(_atom + "published"), out var parsedPublished))
            {
                parsedEntry.Published = parsedPublished;
            }

            if (TryParseAtom10Text(entryElement.Element(_atom + "rights"), out var parsedRights))
            {
                parsedEntry.Rights = parsedRights;
            }

            if (TryParseAtom10Source(entryElement.Element(_atom + "source"), out var parsedSource))
            {
                parsedEntry.Source = parsedSource;
            }

            return true;
        }

        private static bool TryParseAtom10Source(XElement sourceElement, out Atom10Source parsedSource)
        {
            parsedSource = default;

            if (sourceElement == null)
                return false;

            parsedSource = new Atom10Source();

            parsedSource.Id = sourceElement.Element(_atom + "id")?.Value.Trim();

            if (TryParseAtom10Text(sourceElement.Element(_atom + "title"), out var parsedTitle))
            {
                parsedSource.Title = parsedTitle;
            }

            if (TryParseAtom10Timestamp(sourceElement.Element(_atom + "updated"), out var parsedUpdated))
            {
                parsedSource.Updated = parsedUpdated;
            }

            return true;
        }

        private static bool TryParseAtom10Content(XElement contentElement, out Atom10Content parsedContent)
        {
            parsedContent = default;

            if (contentElement == null)
                return false;

            parsedContent = new Atom10Content();

            parsedContent.Type = contentElement.Attribute("type")?.Value ?? "text";
            parsedContent.Src = contentElement.Attribute("src")?.Value;
            parsedContent.Value = contentElement.Value.Trim();

            return true;
        }

        private static bool TryParseAtom10Generator(XElement generatorElement, out Atom10Generator parsedGenerator)
        {
            parsedGenerator = default;

            if (generatorElement == null)
                return false;

            parsedGenerator = new Atom10Generator();

            parsedGenerator.Uri = generatorElement.Attribute("uri")?.Value;
            parsedGenerator.Version = generatorElement.Attribute("version")?.Value;
            parsedGenerator.Value = generatorElement.Value.Trim();

            return true;
        }

        private static bool TryParseAtom10Category(XElement categoryElement, out Atom10Category parsedCategory)
        {
            parsedCategory = default;

            if (categoryElement == null)
                return false;

            parsedCategory = new Atom10Category();

            parsedCategory.Term = categoryElement.Attribute("term")?.Value;
            parsedCategory.Label = categoryElement.Attribute("label")?.Value;
            parsedCategory.Scheme = categoryElement.Attribute("scheme")?.Value;

            return true;
        }

        private static bool TryParseAtom10Link(XElement linkElement, out Atom10Link parsedLink)
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

        private static bool TryParseAtom10Person(XElement personElement, out Atom10Person parsedPerson)
        {
            parsedPerson = default;

            if (personElement == null)
                return false;

            parsedPerson = new Atom10Person();

            parsedPerson.Name = personElement.Element(_atom + "name")?.Value.Trim();
            parsedPerson.Email = personElement.Element(_atom + "email")?.Value.Trim();
            parsedPerson.Uri = personElement.Element(_atom + "uri")?.Value.Trim();

            return true;
        }

        private static bool TryParseAtom10Timestamp(XElement timestampElement, out DateTimeOffset parsedTimestamp)
        {
            parsedTimestamp = default;

            if (timestampElement == null)
                return false;

            if (!Rfc3339TimestampParser.TryParseTimestampFromString(timestampElement.Value, out parsedTimestamp))
                return false;

            return true;
        }

        private static bool TryParseAtom10Text(XElement textElement, out Atom10Text parsedText)
        {
            parsedText = default;

            if (textElement == null)
                return false;

            parsedText = new Atom10Text();

            parsedText.Type = textElement.Attribute("type")?.Value ?? "text";
            parsedText.Value = textElement.Value.Trim();

            return true;
        }
    }
}