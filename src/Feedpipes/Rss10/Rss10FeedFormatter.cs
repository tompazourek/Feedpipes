using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Extensions;
using Feedpipes.Rss10.Entities;
using Feedpipes.Utils.Xml;

namespace Feedpipes.Rss10
{
    public static class Rss10FeedFormatter
    {
        private static readonly XNamespace _rss = Rss10Constants.Namespace;
        private static readonly XNamespace _rdf = Rss10Constants.RdfNamespace;

        public static bool TryFormatRss10Feed(Rss10Feed feed, out XDocument document, ExtensionManifestDirectory extensionManifestDirectory = null)
        {
            document = default;

            if (feed == null)
                return false;

            document = new XDocument();

            var rdfElement = new XElement(_rdf + "RDF");
            document.Add(rdfElement);

            var namespaceAliases = new XNamespaceAliasSet();
            namespaceAliases.EnsureNamespaceAlias(Rss10Constants.RdfNamespaceAlias, _rdf);
            namespaceAliases.EnsureNamespaceAlias(alias: null, _rss);

            if (extensionManifestDirectory == null)
            {
                extensionManifestDirectory = ExtensionManifestDirectory.DefaultForRss;
            }

            if (!TryFormatRss10Channel(feed.Channel, namespaceAliases, extensionManifestDirectory, out var channelElement))
                return false;

            rdfElement.Add(channelElement);

            if (TryFormatRss10Image(feed.Channel.Image, referenceOnly: false, namespaceAliases, extensionManifestDirectory, out var imageElement))
            {
                rdfElement.Add(imageElement);
            }

            if (TryFormatRss10TextInput(feed.Channel.TextInput, referenceOnly: false, namespaceAliases, extensionManifestDirectory, out var textInputElement))
            {
                rdfElement.Add(textInputElement);
            }

            foreach (var itemToFormat in feed.Channel.Items)
            {
                if (TryFormatRss10Item(itemToFormat, referenceOnly: false, namespaceAliases, extensionManifestDirectory, out var itemElement))
                {
                    rdfElement.Add(itemElement);
                }
            }

            foreach (var namespaceAlias in namespaceAliases.OrderBy(x => x.Name.LocalName))
            {
                rdfElement.Add(namespaceAlias);
            }

            return true;
        }

        private static bool TryFormatRss10Channel(Rss10Channel channelToFormat, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out XElement channelElement)
        {
            channelElement = default;

            if (channelToFormat == null)
                return false;

            channelElement = new XElement(_rss + "channel");

            channelElement.Add(new XAttribute(_rdf + "about", channelToFormat.About ?? ""));

            channelElement.Add(new XElement(_rss + "title", channelToFormat.Title ?? ""));
            channelElement.Add(new XElement(_rss + "link", channelToFormat.Link ?? ""));
            channelElement.Add(new XElement(_rss + "description", channelToFormat.Description ?? ""));

            if (TryFormatRss10Image(channelToFormat.Image, referenceOnly: true, namespaceAliases: namespaceAliases, extensionManifestDirectory, out var imageElement))
            {
                channelElement.Add(imageElement);
            }

            if (TryFormatRss10TextInput(channelToFormat.TextInput, referenceOnly: true, namespaceAliases: namespaceAliases, extensionManifestDirectory, out var textInputElement))
            {
                channelElement.Add(textInputElement);
            }

            // extensions
            if (ExtensibleEntityFormatter.TryFormatXElementExtensions(channelToFormat, namespaceAliases, extensionManifestDirectory, out var extensionElements))
            {
                channelElement.AddRange(extensionElements);
            }

            // items
            var liElements = new List<XElement>();
            foreach (var itemToFormat in channelToFormat.Items)
            {
                if (TryFormatRss10Item(itemToFormat, referenceOnly: true, namespaceAliases: namespaceAliases, extensionManifestDirectory, itemElement: out var liElement))
                {
                    liElements.Add(liElement);
                }
            }

            if (liElements.Any())
            {
                var itemsElement = new XElement(_rss + "items");
                var seqElement = new XElement(_rdf + "Seq");
                seqElement.AddRange(liElements);
                itemsElement.Add(seqElement);
                channelElement.Add(itemsElement);
            }

            return true;
        }

        private static bool TryFormatRss10Item(Rss10Item itemToFormat, bool referenceOnly, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out XElement itemElement)
        {
            itemElement = default;

            if (itemToFormat == null)
                return false;

            itemElement = referenceOnly
                ? new XElement(_rdf + "li", new XAttribute("resource", itemToFormat.About ?? ""))
                : new XElement(_rss + "item", new XAttribute(_rdf + "about", itemToFormat.About ?? ""));

            if (referenceOnly)
                return true;

            itemElement.Add(new XElement(_rss + "title") { Value = itemToFormat.Title ?? "" });
            itemElement.Add(new XElement(_rss + "link") { Value = itemToFormat.Link ?? "" });

            if (TryFormatOptionalTextElement(itemToFormat.Description, _rss + "description", out var descriptionElement))
            {
                itemElement.Add(descriptionElement);
            }

            // extensions
            if (ExtensibleEntityFormatter.TryFormatXElementExtensions(itemToFormat, namespaceAliases, extensionManifestDirectory, out var extensionElements))
            {
                itemElement.AddRange(extensionElements);
            }

            return true;
        }

        private static bool TryFormatRss10TextInput(Rss10TextInput textInputToFormat, bool referenceOnly, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out XElement textInputElement)
        {
            textInputElement = default;

            if (textInputToFormat == null)
                return false;

            textInputElement = referenceOnly
                ? new XElement(_rss + "textinput", new XAttribute(_rdf + "resource", textInputToFormat.About ?? ""))
                : new XElement(_rss + "textinput", new XAttribute(_rdf + "about", textInputToFormat.About ?? ""));

            if (referenceOnly)
                return true;

            textInputElement.Add(new XElement(_rss + "title") { Value = textInputToFormat.Title ?? "" });
            textInputElement.Add(new XElement(_rss + "description") { Value = textInputToFormat.Description ?? "" });
            textInputElement.Add(new XElement(_rss + "name") { Value = textInputToFormat.Name ?? "" });
            textInputElement.Add(new XElement(_rss + "link") { Value = textInputToFormat.Link ?? "" });

            // extensions
            if (ExtensibleEntityFormatter.TryFormatXElementExtensions(textInputToFormat, namespaceAliases, extensionManifestDirectory, out var extensionElements))
            {
                textInputElement.AddRange(extensionElements);
            }

            return true;
        }

        private static bool TryFormatRss10Image(Rss10Image imageToFormat, bool referenceOnly, XNamespaceAliasSet namespaceAliases, ExtensionManifestDirectory extensionManifestDirectory, out XElement imageElement)
        {
            imageElement = default;

            if (imageToFormat == null)
                return false;

            imageElement = referenceOnly
                ? new XElement(_rss + "image", new XAttribute(_rdf + "resource", imageToFormat.About ?? ""))
                : new XElement(_rss + "image", new XAttribute(_rdf + "about", imageToFormat.About ?? ""));

            if (referenceOnly)
                return true;

            imageElement.Add(new XElement(_rss + "title") { Value = imageToFormat.Title ?? "" });
            imageElement.Add(new XElement(_rss + "url") { Value = imageToFormat.Url ?? "" });
            imageElement.Add(new XElement(_rss + "link") { Value = imageToFormat.Link ?? "" });

            // extensions
            if (ExtensibleEntityFormatter.TryFormatXElementExtensions(imageToFormat, namespaceAliases, extensionManifestDirectory, out var extensionElements))
            {
                imageElement.AddRange(extensionElements);
            }

            return true;
        }

        private static bool TryFormatOptionalTextElement(string stringToFormat, XName elementName, out XElement element)
        {
            element = default;

            if (string.IsNullOrEmpty(stringToFormat))
                return false;

            element = new XElement(elementName) { Value = stringToFormat };
            return true;
        }
    }
}