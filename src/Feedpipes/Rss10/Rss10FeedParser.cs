using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions;
using Feedpipes.Syndication.Rss10.Entities;

namespace Feedpipes.Syndication.Rss10
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class Rss10FeedParser
    {
        private static readonly XNamespace _rdf = Rss10Constants.RdfNamespace;

        public static bool TryParseRss10Feed(XDocument document, out Rss10Feed parsedFeed, ExtensionManifestDirectory extensionManifestDirectory = null)
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

            if (extensionManifestDirectory == null)
            {
                extensionManifestDirectory = ExtensionManifestDirectory.DefaultForRss;
            }

            if (!TryParseRss10Channel(rdfElement.Element(rss + "channel"), rss, extensionManifestDirectory, out var parsedChannel))
                return false;

            if (TryParseRss10Image(rdfElement.Element(rss + "image"), rss, extensionManifestDirectory, out var parsedImage))
            {
                parsedChannel.Image = parsedImage;
            }

            if (TryParseRss10TextInput(rdfElement.Element(rss + "textinput"), rss, extensionManifestDirectory, out var parsedTextInput))
            {
                parsedChannel.TextInput = parsedTextInput;
            }

            // items
            foreach (var itemElement in rdfElement.Elements(rss + "item"))
            {
                if (TryParseRss10Item(itemElement, rss, extensionManifestDirectory, out var parsedItem))
                {
                    parsedChannel.Items.Add(parsedItem);
                }
            }

            parsedFeed = new Rss10Feed();
            parsedFeed.Channel = parsedChannel;
            return true;
        }

        private static bool TryParseRss10Channel(XElement channelElement, XNamespace rss, ExtensionManifestDirectory extensionManifestDirectory, out Rss10Channel parsedChannel)
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
            ExtensibleEntityParser.ParseXElementExtensions(channelElement, extensionManifestDirectory, parsedChannel);

            return true;
        }

        private static bool TryParseRss10Image(XElement imageElement, XNamespace rss, ExtensionManifestDirectory extensionManifestDirectory, out Rss10Image parsedImage)
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
            ExtensibleEntityParser.ParseXElementExtensions(imageElement, extensionManifestDirectory, parsedImage);

            return true;
        }

        private static bool TryParseRss10TextInput(XElement textInputElement, XNamespace rss, ExtensionManifestDirectory extensionManifestDirectory, out Rss10TextInput parsedTextInput)
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
            ExtensibleEntityParser.ParseXElementExtensions(textInputElement, extensionManifestDirectory, parsedTextInput);

            return true;
        }

        private static bool TryParseRss10Item(XElement itemElement, XNamespace rss, ExtensionManifestDirectory extensionManifestDirectory, out Rss10Item parsedItem)
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
            ExtensibleEntityParser.ParseXElementExtensions(itemElement, extensionManifestDirectory, parsedItem);

            return true;
        }
    }
}