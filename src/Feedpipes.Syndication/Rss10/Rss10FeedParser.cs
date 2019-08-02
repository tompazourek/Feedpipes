using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.CreativeCommons;
using Feedpipes.Syndication.Extensions.DublinCore;
using Feedpipes.Syndication.Extensions.Rss10Content;
using Feedpipes.Syndication.Extensions.Rss10Slash;
using Feedpipes.Syndication.Extensions.Rss10Syndication;
using Feedpipes.Syndication.Extensions.RssAtom10;
using Feedpipes.Syndication.Extensions.WellFormedWeb;
using Feedpipes.Syndication.Rss10.Entities;

namespace Feedpipes.Syndication.Rss10
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class Rss10FeedParser
    {
        private static readonly XNamespace _rdf = Rss10Constants.RdfNamespace;

        public static bool TryParseRss10Feed(XDocument document, out Rss10Feed parsedFeed)
        {
            parsedFeed = default;

            var rdfElement = document?.Element(_rdf + "RDF");
            if (rdfElement == null)
                return false;

            var rssNamespace = rdfElement.Attribute("xmlns")?.Value;
            XNamespace rss = null;
            foreach (var ns in Rss10Constants.RecognizedNamespaces)
            {
                rss = ns;
                if (rssNamespace == ns.NamespaceName)
                    break;
            }

            if (rss == null)
                return false;

            if (!TryParseRss10Channel(rdfElement.Element(rss + "channel"), rss, out var parsedChannel))
                return false;

            if (TryParseRss10Image(rdfElement.Element(rss + "image"), rss, out var parsedImage))
            {
                parsedChannel.Image = parsedImage;
            }

            if (TryParseRss10TextInput(rdfElement.Element(rss + "textinput"), rss, out var parsedTextInput))
            {
                parsedChannel.TextInput = parsedTextInput;
            }

            // items
            foreach (var itemElement in rdfElement.Elements(rss + "item"))
            {
                if (TryParseRss10Item(itemElement, rss, out var parsedItem))
                {
                    parsedChannel.Items.Add(parsedItem);
                }
            }

            parsedFeed = new Rss10Feed();
            parsedFeed.Channel = parsedChannel;
            return true;
        }

        private static bool TryParseRss10Channel(XElement channelElement, XNamespace rss, out Rss10Channel parsedChannel)
        {
            parsedChannel = default;

            if (channelElement == null)
                return false;

            parsedChannel = new Rss10Channel();
            parsedChannel.About = channelElement.Attribute(_rdf + "about")?.Value;
            parsedChannel.Title = channelElement.Element(rss + "title")?.Value.Trim();
            parsedChannel.Link = channelElement.Element(rss + "link")?.Value.Trim();
            parsedChannel.Description = channelElement.Element(rss + "description")?.Value.Trim();

            // extensions
            if (Rss10SyndicationChannelExtensionParser.TryParseRss10SyndicationChannelExtension(channelElement, out var parsedSyndicationExtension))
            {
                parsedChannel.SyndicationExtension = parsedSyndicationExtension;
            }

            if (DublinCoreElementExtensionParser.TryParseDublinCoreElementExtension(channelElement, out var parsedDublinCoreExtension))
            {
                parsedChannel.DublinCoreExtension = parsedDublinCoreExtension;
            }
            
            if (RssAtom10ElementExtensionParser.TryParseRssAtom10ElementExtension(channelElement, out var parsedRssAtom10Extension))
            {
                parsedChannel.RssAtom10Extension = parsedRssAtom10Extension;
            }

            if (CreativeCommonsElementExtensionParser.TryParseCreativeCommonsElementExtension(channelElement, out var parsedCreativeCommonsExtension))
            {
                parsedChannel.CreativeCommonsExtension = parsedCreativeCommonsExtension;
            }

            return true;
        }

        private static bool TryParseRss10Image(XElement imageElement, XNamespace rss, out Rss10Image parsedImage)
        {
            parsedImage = default;

            if (imageElement == null)
                return false;

            parsedImage = new Rss10Image();
            parsedImage.About = imageElement.Attribute(_rdf + "about")?.Value;
            parsedImage.Title = imageElement.Element(rss + "title")?.Value.Trim();
            parsedImage.Url = imageElement.Element(rss + "url")?.Value.Trim();
            parsedImage.Link = imageElement.Element(rss + "link")?.Value.Trim();

            // extensions
            if (DublinCoreElementExtensionParser.TryParseDublinCoreElementExtension(imageElement, out var parsedDublinCoreExtension))
            {
                parsedImage.DublinCoreExtension = parsedDublinCoreExtension;
            }

            return true;
        }

        private static bool TryParseRss10TextInput(XElement textInputElement, XNamespace rss, out Rss10TextInput parsedTextInput)
        {
            parsedTextInput = default;

            if (textInputElement == null)
                return false;

            parsedTextInput = new Rss10TextInput();
            parsedTextInput.About = textInputElement.Attribute(_rdf + "about")?.Value;
            parsedTextInput.Title = textInputElement.Element(rss + "title")?.Value.Trim();
            parsedTextInput.Description = textInputElement.Element(rss + "description")?.Value.Trim();
            parsedTextInput.Name = textInputElement.Element(rss + "name")?.Value.Trim();
            parsedTextInput.Link = textInputElement.Element(rss + "link")?.Value.Trim();

            // extensions
            if (DublinCoreElementExtensionParser.TryParseDublinCoreElementExtension(textInputElement, out var parsedDublinCoreExtension))
            {
                parsedTextInput.DublinCoreExtension = parsedDublinCoreExtension;
            }

            return true;
        }

        private static bool TryParseRss10Item(XElement itemElement, XNamespace rss, out Rss10Item parsedItem)
        {
            parsedItem = default;

            if (itemElement == null)
                return false;

            parsedItem = new Rss10Item();
            parsedItem.About = itemElement.Attribute(_rdf + "about")?.Value;
            parsedItem.Title = itemElement.Element(rss + "title")?.Value.Trim();
            parsedItem.Link = itemElement.Element(rss + "link")?.Value.Trim();
            parsedItem.Description = itemElement.Element(rss + "description")?.Value.Trim();

            // extensions
            if (Rss10ContentItemExtensionParser.TryParseRss10ContentItemExtension(itemElement, out var parsedContentExtension))
            {
                parsedItem.ContentExtension = parsedContentExtension;
            }

            if (Rss10SlashItemExtensionParser.TryParseRss10SlashItemExtension(itemElement, out var parsedSlashExtension))
            {
                parsedItem.SlashExtension = parsedSlashExtension;
            }

            if (WfwItemExtensionParser.TryParseWfwItemExtension(itemElement, out var parsedWfwExtension))
            {
                parsedItem.WfwExtension = parsedWfwExtension;
            }

            if (DublinCoreElementExtensionParser.TryParseDublinCoreElementExtension(itemElement, out var parsedDublinCoreExtension))
            {
                parsedItem.DublinCoreExtension = parsedDublinCoreExtension;
            }
            
            if (RssAtom10ElementExtensionParser.TryParseRssAtom10ElementExtension(itemElement, out var parsedRssAtom10Extension))
            {
                parsedItem.RssAtom10Extension = parsedRssAtom10Extension;
            }

            if (CreativeCommonsElementExtensionParser.TryParseCreativeCommonsElementExtension(itemElement, out var parsedCreativeCommonsExtension))
            {
                parsedItem.CreativeCommonsExtension = parsedCreativeCommonsExtension;
            }

            return true;
        }
    }
}