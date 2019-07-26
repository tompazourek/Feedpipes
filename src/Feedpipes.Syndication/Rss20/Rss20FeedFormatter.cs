﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions;
using Feedpipes.Syndication.Rfc822Timestamp;
using Feedpipes.Syndication.Rss20.Entities;

namespace Feedpipes.Syndication.Rss20
{
    public class Rss20FeedFormatter
    {
        private readonly Rfc822TimestampFormatter _timestampFormatter;
        private readonly AbstractFeedExtensionEntityFormatter _extensionFormatter;

        public Rss20FeedFormatter()
        {
            _timestampFormatter = new Rfc822TimestampFormatter();
            _extensionFormatter = new AbstractFeedExtensionEntityFormatter();
        }

        public bool TryFormatRss20Feed(Rss20Feed feed, out XDocument document)
        {
            document = default;

            if (feed == null)
                return false;

            document = new XDocument();

            var rssElement = new XElement("rss", new XAttribute("version", "2.0"));
            document.Add(rssElement);

            if (!TryFormatRss20Channel(feed.Channel, out var channelElement, out var rootNamespaceAliases))
                return false;

            rssElement.Add(channelElement);

            foreach (var rootNamespaceAlias in rootNamespaceAliases.GroupBy(x => x.Name).Select(x => x.First()))
            {
                rssElement.Add(rootNamespaceAlias);
            }

            return true;
        }

        private bool TryFormatRss20Channel(Rss20Channel channelToFormat, out XElement channelElement, out IEnumerable<XAttribute> rootNamespaceAliases)
        {
            channelElement = default;
            rootNamespaceAliases = default;

            if (channelToFormat == null)
                return false;

            channelElement = new XElement("channel");

            channelElement.Add(new XElement("title", channelToFormat.Title));
            channelElement.Add(new XElement("link", channelToFormat.Link));
            channelElement.Add(new XElement("description", channelToFormat.Description));

            if (TryFormatOptionalTextElement(channelToFormat.Language, "language", out var languageElement))
            {
                channelElement.Add(languageElement);
            }

            if (TryFormatOptionalTextElement(channelToFormat.Copyright, "copyright", out var copyrightElement))
            {
                channelElement.Add(copyrightElement);
            }

            if (TryFormatOptionalTextElement(channelToFormat.ManagingEditor, "managingEditor", out var managingEditorElement))
            {
                channelElement.Add(managingEditorElement);
            }

            if (TryFormatOptionalTextElement(channelToFormat.WebMaster, "webMaster", out var webMasterElement))
            {
                channelElement.Add(webMasterElement);
            }

            if (TryFormatOptionalTextElement(channelToFormat.Generator, "generator", out var generatorElement))
            {
                channelElement.Add(generatorElement);
            }

            if (TryFormatOptionalTextElement(channelToFormat.Docs, "docs", out var docsElement))
            {
                channelElement.Add(docsElement);
            }

            if (TryFormatRss20Timestamp(channelToFormat.PubDate, "pubDate", out var pubDateElement))
            {
                channelElement.Add(pubDateElement);
            }

            if (TryFormatRss20Timestamp(channelToFormat.LastBuildDate, "lastBuildDate", out var lastBuildDateElement))
            {
                channelElement.Add(lastBuildDateElement);
            }

            foreach (var categoryToFormat in channelToFormat.Categories)
            {
                if (TryFormatRss20Category(categoryToFormat, out var categoryElement))
                {
                    channelElement.Add(categoryElement);
                }
            }

            if (TryFormatRss20Cloud(channelToFormat.Cloud, out var cloudElement))
            {
                channelElement.Add(cloudElement);
            }

            if (TryFormatRss20Ttl(channelToFormat.Ttl, out var ttlElement))
            {
                channelElement.Add(ttlElement);
            }

            if (TryFormatRss20Image(channelToFormat.Image, out var imageElement))
            {
                channelElement.Add(imageElement);
            }

            if (TryFormatRss20TextInput(channelToFormat.TextInput, out var textInputElement))
            {
                channelElement.Add(textInputElement);
            }

            if (TryFormatRss20SkipHours(channelToFormat.SkipHours, out var skipHoursElement))
            {
                channelElement.Add(skipHoursElement);
            }

            if (TryFormatRss20SkipDays(channelToFormat.SkipDays, out var skipDaysElement))
            {
                channelElement.Add(skipDaysElement);
            }

            var rootNamespaceAliasesList = new List<XAttribute>();
            
            // extensions
            var extensionElements = _extensionFormatter.FormatExtensionEntities(channelToFormat.Extensions, out var channelRootNamespaceAliases);
            foreach (var extensionElement in extensionElements)
            {
                channelElement.Add(extensionElement);
            }
            rootNamespaceAliasesList.AddRange(channelRootNamespaceAliases);

            // items
            foreach (var itemToFormat in channelToFormat.Items)
            {
                if (TryFormatRss20Item(itemToFormat, out var itemElement, out var itemRootNamespaceAliases))
                {
                    channelElement.Add(itemElement);
                    rootNamespaceAliasesList.AddRange(itemRootNamespaceAliases);
                }
            }
            
            rootNamespaceAliases = rootNamespaceAliasesList;
            return true;
        }

        private bool TryFormatRss20Item(Rss20Item itemToFormat, out XElement itemElement, out IEnumerable<XAttribute> rootNamespaceAliases)
        {
            itemElement = default;
            rootNamespaceAliases = default;

            if (itemToFormat == null)
                return false;

            itemElement = new XElement("item");

            if (TryFormatOptionalTextElement(itemToFormat.Title, "title", out var titleElement))
            {
                itemElement.Add(titleElement);
            }

            if (TryFormatOptionalTextElement(itemToFormat.Link, "link", out var linkElement))
            {
                itemElement.Add(linkElement);
            }

            if (TryFormatOptionalTextElement(itemToFormat.Description, "description", out var descriptionElement))
            {
                itemElement.Add(descriptionElement);
            }

            if (TryFormatOptionalTextElement(itemToFormat.Author, "author", out var authorElement))
            {
                itemElement.Add(authorElement);
            }

            if (TryFormatOptionalTextElement(itemToFormat.Comments, "comments", out var commentsElement))
            {
                itemElement.Add(commentsElement);
            }

            foreach (var categoryToFormat in itemToFormat.Categories)
            {
                if (TryFormatRss20Category(categoryToFormat, out var categoryElement))
                {
                    itemElement.Add(categoryElement);
                }
            }

            foreach (var enclosureToFormat in itemToFormat.Enclosures)
            {
                if (TryFormatRss20Enclosure(enclosureToFormat, out var enclosureElement))
                {
                    itemElement.Add(enclosureElement);
                }
            }

            if (TryFormatRss20Timestamp(itemToFormat.PubDate, "pubDate", out var pubDateElement))
            {
                itemElement.Add(pubDateElement);
            }

            if (TryFormatRss20Source(itemToFormat.Source, out var sourceElement))
            {
                itemElement.Add(sourceElement);
            }

            if (TryFormatRss20Guid(itemToFormat.Guid, out var guidElement))
            {
                itemElement.Add(guidElement);
            }

            // extensions
            var extensionElements = _extensionFormatter.FormatExtensionEntities(itemToFormat.Extensions, out rootNamespaceAliases);
            foreach (var extensionElement in extensionElements)
            {
                itemElement.Add(extensionElement);
            }

            return true;
        }

        private bool TryFormatRss20Source(Rss20Source sourceToFormat, out XElement sourceElement)
        {
            sourceElement = default;

            if (sourceToFormat == null)
                return false;

            if (!TryFormatOptionalTextElement(sourceToFormat.Name, "source", out sourceElement))
                return false;

            if (TryFormatOptionalTextAttribute(sourceToFormat.Url, "url", out var sourceUrlAttribute))
            {
                sourceElement.Add(sourceUrlAttribute);
            }

            return true;
        }

        private bool TryFormatRss20Guid(Rss20Guid guidToFormat, out XElement guidElement)
        {
            guidElement = default;

            if (guidToFormat == null)
                return false;

            if (!TryFormatOptionalTextElement(guidToFormat.Value, "guid", out guidElement))
                return false;

            if (TryFormatOptionalBoolAttribute(guidToFormat.IsPermaLink, "isPermaLink", out var guidIsPermaLinkAttribute))
            {
                guidElement.Add(guidIsPermaLinkAttribute);
            }

            return true;
        }

        private bool TryFormatRss20Enclosure(Rss20Enclosure enclosureToFormat, out XElement enclosureElement)
        {
            enclosureElement = default;

            if (enclosureToFormat == null)
                return false;

            enclosureElement = new XElement("enclosure");
            enclosureElement.Add(new XAttribute("url", enclosureToFormat.Url));
            enclosureElement.Add(new XAttribute("length", enclosureToFormat.Length));
            enclosureElement.Add(new XAttribute("type", enclosureToFormat.Type));

            return true;
        }

        private bool TryFormatRss20SkipDays(IList<DayOfWeek> skipDaysToFormat, out XElement skipDaysElement)
        {
            skipDaysElement = default;

            if (skipDaysToFormat == null)
                return false;

            if (!skipDaysToFormat.Any())
                return false;

            skipDaysElement = new XElement("skipDays");

            foreach (var dayOfWeek in skipDaysToFormat)
            {
                string dayOfWeekString;
                switch (dayOfWeek)
                {
                    case DayOfWeek.Friday:
                        dayOfWeekString = "Friday";
                        break;
                    case DayOfWeek.Monday:
                        dayOfWeekString = "Monday";
                        break;
                    case DayOfWeek.Saturday:
                        dayOfWeekString = "Saturday";
                        break;
                    case DayOfWeek.Sunday:
                        dayOfWeekString = "Sunday";
                        break;
                    case DayOfWeek.Thursday:
                        dayOfWeekString = "Thursday";
                        break;
                    case DayOfWeek.Tuesday:
                        dayOfWeekString = "Tuesday";
                        break;
                    case DayOfWeek.Wednesday:
                        dayOfWeekString = "Wednesday";
                        break;
                    default:
                        continue;
                }

                var dayElement = new XElement("day") { Value = dayOfWeekString };
                skipDaysElement.Add(dayElement);
            }

            return true;
        }

        private bool TryFormatRss20SkipHours(IList<int> skipHoursToFormat, out XElement skipHoursElement)
        {
            skipHoursElement = default;

            if (skipHoursToFormat == null)
                return false;

            if (!skipHoursToFormat.Any())
                return false;

            skipHoursElement = new XElement("skipHours");

            foreach (var hour in skipHoursToFormat)
            {
                var hourString = hour.ToString(CultureInfo.InvariantCulture);
                var hourElement = new XElement("hour") { Value = hourString };
                skipHoursElement.Add(hourElement);
            }

            return true;
        }

        private bool TryFormatRss20TextInput(Rss20TextInput textInputToFormat, out XElement textInputElement)
        {
            textInputElement = default;

            if (textInputToFormat == null)
                return false;

            textInputElement = new XElement("textInput");

            textInputElement.Add(new XElement("title") { Value = textInputToFormat.Title });
            textInputElement.Add(new XElement("description") { Value = textInputToFormat.Description });
            textInputElement.Add(new XElement("name") { Value = textInputToFormat.Name });
            textInputElement.Add(new XElement("link") { Value = textInputToFormat.Link });

            return true;
        }

        private bool TryFormatRss20Image(Rss20Image imageToFormat, out XElement imageElement)
        {
            imageElement = default;

            if (imageToFormat == null)
                return false;

            imageElement = new XElement("image");

            imageElement.Add(new XElement("url") { Value = imageToFormat.Url });
            imageElement.Add(new XElement("title") { Value = imageToFormat.Title });
            imageElement.Add(new XElement("link") { Value = imageToFormat.Link });

            if (imageToFormat.Height != null)
            {
                var heightString = imageToFormat.Height.Value.ToString(CultureInfo.InvariantCulture);
                imageElement.Add(new XElement("height") { Value = heightString });
            }

            if (imageToFormat.Width != null)
            {
                var widthString = imageToFormat.Width.Value.ToString(CultureInfo.InvariantCulture);
                imageElement.Add(new XElement("width") { Value = widthString });
            }

            if (TryFormatOptionalTextElement(imageToFormat.Description, "description", out var descriptionElement))
            {
                imageElement.Add(descriptionElement);
            }

            return true;
        }

        private bool TryFormatRss20Ttl(TimeSpan? ttlToFormat, out XElement ttlElement)
        {
            ttlElement = default;

            if (ttlToFormat == null)
                return false;

            var totalMinutesString = ((int) ttlToFormat.Value.TotalMinutes).ToString(CultureInfo.InvariantCulture);
            ttlElement = new XElement("ttl") { Value = totalMinutesString };

            return true;
        }

        private bool TryFormatRss20Timestamp(DateTimeOffset? timestampToFormat, XName elementName, out XElement element)
        {
            element = default;

            if (!_timestampFormatter.TryFormatTimestampAsString(timestampToFormat, out var timestampString))
                return false;

            element = new XElement(elementName, timestampString);
            return true;
        }

        private bool TryFormatOptionalTextElement(string stringToFormat, XName elementName, out XElement element)
        {
            element = default;

            if (string.IsNullOrEmpty(stringToFormat))
                return false;

            element = new XElement(elementName) { Value = stringToFormat };
            return true;
        }

        private bool TryFormatOptionalTextAttribute(string stringToFormat, XName attributeName, out XAttribute attribute)
        {
            attribute = default;

            if (string.IsNullOrEmpty(stringToFormat))
                return false;

            attribute = new XAttribute(attributeName, stringToFormat);
            return true;
        }

        private bool TryFormatOptionalBoolAttribute(bool? boolToFormat, XName attributeName, out XAttribute attribute)
        {
            attribute = default;

            if (boolToFormat == null)
                return false;

            attribute = new XAttribute(attributeName, boolToFormat.Value ? "true" : "false");
            return true;
        }

        private bool TryFormatRss20Category(Rss20Category categoryToFormat, out XElement categoryElement)
        {
            categoryElement = default;

            if (categoryToFormat == null)
                return false;

            if (!TryFormatOptionalTextElement(categoryToFormat.Name, "category", out categoryElement))
                return false;

            if (TryFormatOptionalTextAttribute(categoryToFormat.Domain, "domain", out var categoryDomainAttribute))
            {
                categoryElement.Add(categoryDomainAttribute);
            }

            return true;
        }

        private bool TryFormatRss20Cloud(Rss20Cloud cloudToFormat, out XElement cloudElement)
        {
            cloudElement = default;

            if (cloudToFormat == null)
                return false;

            cloudElement = new XElement("cloud");
            cloudElement.Add(new XAttribute("domain", cloudToFormat.Domain));
            cloudElement.Add(new XAttribute("port", cloudToFormat.Port));
            cloudElement.Add(new XAttribute("path", cloudToFormat.Path));
            cloudElement.Add(new XAttribute("registerProcedure", cloudToFormat.RegisterProcedure));
            cloudElement.Add(new XAttribute("protocol", cloudToFormat.Protocol));

            return true;
        }
    }
}