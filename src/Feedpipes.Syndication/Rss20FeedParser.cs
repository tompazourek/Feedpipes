using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Rss20;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public class Rss20FeedParser
    {
        public bool TryParseRss20Feed(XDocument document, out Rss20Feed parsedFeed)
        {
            parsedFeed = default;

            var rssElement = document?.Element("rss");
            if (rssElement == null)
                return false;

            var rssVersion = rssElement.Attribute("version")?.Value;
            if (rssVersion != "2.0")
                return false;

            var channelElement = rssElement.Element("channel");
            if (!TryParseRss20Channel(channelElement, out var parsedChannel))
                return false;

            parsedFeed = new Rss20Feed();
            parsedFeed.Channel = parsedChannel;
            return true;
        }

        private bool TryParseRss20Channel(XElement channelElement, out Rss20Channel parsedChannel)
        {
            parsedChannel = default;

            if (channelElement == null)
                return false;

            parsedChannel = new Rss20Channel();
            parsedChannel.Title = channelElement.Element("title")?.Value.Trim();
            parsedChannel.Link = channelElement.Element("link")?.Value.Trim();
            parsedChannel.Description = channelElement.Element("description")?.Value.Trim();
            parsedChannel.Language = channelElement.Element("language")?.Value.Trim();
            parsedChannel.Copyright = channelElement.Element("copyright")?.Value.Trim();
            parsedChannel.ManagingEditor = channelElement.Element("managingEditor")?.Value.Trim();
            parsedChannel.WebMaster = channelElement.Element("webMaster")?.Value.Trim();
            parsedChannel.Generator = channelElement.Element("generator")?.Value.Trim();
            parsedChannel.Docs = channelElement.Element("docs")?.Value.Trim();

            if (TryParseRss20Timestamp(channelElement.Element("pubDate"), out var parsedPubDate))
            {
                parsedChannel.PubDate = parsedPubDate;
            }

            if (TryParseRss20Timestamp(channelElement.Element("lastBuildDate"), out var parsedLastBuildDate))
            {
                parsedChannel.LastBuildDate = parsedLastBuildDate;
            }

            foreach (var categoryElement in channelElement.Elements("category"))
            {
                if (TryParseRss20Category(categoryElement, out var parsedCategory))
                {
                    parsedChannel.Categories.Add(parsedCategory);
                }
            }

            if (TryParseRss20Cloud(channelElement.Element("cloud"), out var parsedCloud))
            {
                parsedChannel.Cloud = parsedCloud;
            }

            if (TryParseRss20Ttl(channelElement.Element("ttl"), out var parsedTtl))
            {
                parsedChannel.Ttl = parsedTtl;
            }

            if (TryParseRss20Image(channelElement.Element("image"), out var parsedImage))
            {
                parsedChannel.Image = parsedImage;
            }

            if (TryParseRss20TextInput(channelElement.Element("textInput"), out var parsedTextInput))
            {
                parsedChannel.TextInput = parsedTextInput;
            }

            if (TryParseRss20SkipHours(channelElement.Element("skipHours"), out var parsedSkipHours))
            {
                parsedChannel.SkipHours = parsedSkipHours;
            }

            if (TryParseRss20SkipDays(channelElement.Element("skipDays"), out var parsedSkipDays))
            {
                parsedChannel.SkipDays = parsedSkipDays;
            }

            foreach (var itemElement in channelElement.Elements("item"))
            {
                if (TryParseRss20Item(itemElement, out var parsedItem))
                {
                    parsedChannel.Items.Add(parsedItem);
                }
            }

            return true;
        }

        private bool TryParseRss20Category(XElement categoryElement, out Rss20Category parsedCategory)
        {
            parsedCategory = default;

            if (categoryElement == null)
                return false;

            parsedCategory = new Rss20Category();
            parsedCategory.Name = categoryElement.Value.Trim();
            parsedCategory.Domain = categoryElement.Attribute("domain")?.Value;

            return true;
        }

        private bool TryParseRss20Timestamp(XElement timestampElement, out DateTimeOffset parsedTimestamp)
        {
            parsedTimestamp = default;

            if (timestampElement == null)
                return false;

            if (!Rfc822TimestampParser.TryParseTimestampFromString(timestampElement.Value, out parsedTimestamp))
                return false;

            return true;
        }

        private bool TryParseRss20Cloud(XElement cloudElement, out Rss20Cloud parsedCloud)
        {
            parsedCloud = default;

            if (cloudElement == null)
                return false;

            parsedCloud = new Rss20Cloud();
            parsedCloud.Domain = cloudElement.Attribute("domain")?.Value;
            parsedCloud.Port = cloudElement.Attribute("port")?.Value;
            parsedCloud.Path = cloudElement.Attribute("path")?.Value;
            parsedCloud.RegisterProcedure = cloudElement.Attribute("registerProcedure")?.Value;
            parsedCloud.Protocol = cloudElement.Attribute("protocol")?.Value;

            return true;
        }

        private bool TryParseRss20Image(XElement imageElement, out Rss20Image parsedImage)
        {
            parsedImage = default;

            if (imageElement == null)
                return false;

            parsedImage = new Rss20Image();
            parsedImage.Url = imageElement.Element("url")?.Value.Trim();
            parsedImage.Title = imageElement.Element("title")?.Value.Trim();
            parsedImage.Link = imageElement.Element("link")?.Value.Trim();
            parsedImage.Description = imageElement.Element("description")?.Value.Trim();

            if (int.TryParse(imageElement.Element("width")?.Value.Trim(), out var parsedWidth))
            {
                parsedImage.Width = parsedWidth;
            }

            if (int.TryParse(imageElement.Element("height")?.Value.Trim(), out var parsedHeight))
            {
                parsedImage.Height = parsedHeight;
            }

            return true;
        }

        private bool TryParseRss20TextInput(XElement textInputElement, out Rss20TextInput parsedTextInput)
        {
            parsedTextInput = default;

            if (textInputElement == null)
                return false;

            parsedTextInput = new Rss20TextInput();
            parsedTextInput.Title = textInputElement.Element("title")?.Value.Trim();
            parsedTextInput.Description = textInputElement.Element("description")?.Value.Trim();
            parsedTextInput.Name = textInputElement.Element("name")?.Value.Trim();
            parsedTextInput.Link = textInputElement.Element("link")?.Value.Trim();

            return true;
        }

        private bool TryParseRss20Ttl(XElement ttlElement, out TimeSpan parsedTtl)
        {
            parsedTtl = default;

            if (ttlElement == null)
                return false;

            if (!double.TryParse(ttlElement.Value.Trim(), out var ttlMinutes))
                return false;

            parsedTtl = TimeSpan.FromMinutes(ttlMinutes);

            return true;
        }

        private bool TryParseRss20SkipHours(XElement skipHoursElement, out IList<int> parsedSkipHours)
        {
            parsedSkipHours = default;

            if (skipHoursElement == null)
                return false;

            parsedSkipHours = new List<int>();

            var hourElements = skipHoursElement.Elements("hour");
            foreach (var hourElement in hourElements)
            {
                if (int.TryParse(hourElement.Value.Trim(), out var parsedHour))
                {
                    parsedSkipHours.Add(parsedHour);
                }
            }

            if (!parsedSkipHours.Any())
                return false;

            return true;
        }

        private bool TryParseRss20SkipDays(XElement skipDaysElement, out IList<DayOfWeek> parsedSkipDays)
        {
            parsedSkipDays = default;

            if (skipDaysElement == null)
                return false;

            parsedSkipDays = new List<DayOfWeek>();
            var dayElements = skipDaysElement.Elements("day");
            foreach (var dayElement in dayElements)
            {
                var dayOfWeekString = dayElement.Value.Trim().ToLowerInvariant();
                DayOfWeek parsedDayOfWeek;
                switch (dayOfWeekString)
                {
                    case "monday":
                        parsedDayOfWeek = DayOfWeek.Monday;
                        break;
                    case "tuesday":
                        parsedDayOfWeek = DayOfWeek.Tuesday;
                        break;
                    case "wednesday":
                        parsedDayOfWeek = DayOfWeek.Wednesday;
                        break;
                    case "thursday":
                        parsedDayOfWeek = DayOfWeek.Thursday;
                        break;
                    case "friday":
                        parsedDayOfWeek = DayOfWeek.Friday;
                        break;
                    case "saturday":
                        parsedDayOfWeek = DayOfWeek.Saturday;
                        break;
                    case "sunday":
                        parsedDayOfWeek = DayOfWeek.Sunday;
                        break;
                    default:
                        continue;
                }

                parsedSkipDays.Add(parsedDayOfWeek);
            }

            if (!parsedSkipDays.Any())
                return false;

            return true;
        }

        private bool TryParseRss20Item(XElement itemElement, out Rss20Item parsedItem)
        {
            parsedItem = default;

            if (itemElement == null)
                return false;

            parsedItem = new Rss20Item();
            parsedItem.Title = itemElement.Element("title")?.Value.Trim();
            parsedItem.Link = itemElement.Element("link")?.Value.Trim();
            parsedItem.Description = itemElement.Element("description")?.Value.Trim();
            parsedItem.Author = itemElement.Element("author")?.Value.Trim();
            parsedItem.Comments = itemElement.Element("comments")?.Value.Trim();
            parsedItem.Guid = itemElement.Element("guid")?.Value.Trim();
            
            foreach (var categoryElement in itemElement.Elements("category"))
            {
                if (TryParseRss20Category(categoryElement, out var parsedCategory))
                {
                    parsedItem.Categories.Add(parsedCategory);
                }
            }
            
            foreach (var enclosureElement in itemElement.Elements("enclosure"))
            {
                if (TryParseRss20Enclosure(enclosureElement, out var parsedEnclosure))
                {
                    parsedItem.Enclosures.Add(parsedEnclosure);
                }
            }
            
            if (TryParseRss20Timestamp(itemElement.Element("pubDate"), out var parsedPubDate))
            {
                parsedItem.PubDate = parsedPubDate;
            }

            if (TryParseRss20Source(itemElement.Element("source"), out var parsedSource))
            {
                parsedItem.Source = parsedSource;
            }

            return true;
        }

        private bool TryParseRss20Source(XElement sourceElement, out Rss20Source parsedSource)
        {
            parsedSource = default;

            if (sourceElement == null)
                return false;

            parsedSource = new Rss20Source();
            parsedSource.Name = sourceElement.Value.Trim();
            parsedSource.Url = sourceElement.Attribute("url")?.Value;

            return true;
        }

        private bool TryParseRss20Enclosure(XElement enclosureElement, out Rss20Enclosure parsedEnclosure)
        {
            parsedEnclosure = default;

            if (enclosureElement == null)
                return false;

            parsedEnclosure = new Rss20Enclosure();
            parsedEnclosure.Url = enclosureElement.Attribute("url")?.Value;
            parsedEnclosure.Length = enclosureElement.Attribute("length")?.Value;
            parsedEnclosure.Type = enclosureElement.Attribute("type")?.Value;

            return true;
        }

    }
}