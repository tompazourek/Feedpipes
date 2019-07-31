using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.DublinCore;
using Feedpipes.Syndication.Extensions.Rss10Content;
using Feedpipes.Syndication.Extensions.Rss10Slash;
using Feedpipes.Syndication.Extensions.Rss10Syndication;
using Feedpipes.Syndication.Extensions.WellFormedWeb;
using Feedpipes.Syndication.Rss10.Entities;

namespace Feedpipes.Syndication.Rss10
{
    public static class Rss10FeedFormatter
    {
        private static readonly XNamespace _rss = Rss10Constants.Rss10Namespace;
        private static readonly XNamespace _rdf = Rss10Constants.RdfNamespace;

        public static bool TryFormatRss10Feed(Rss10Feed feed, out XDocument document)
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

            if (!TryFormatRss10Channel(feed.Channel, namespaceAliases, out var channelElement))
                return false;

            rdfElement.Add(channelElement);

            if (TryFormatRss10Image(feed.Channel.Image, referenceOnly: false, namespaceAliases, out var imageElement))
            {
                rdfElement.Add(imageElement);
            }
            
            if (TryFormatRss10TextInput(feed.Channel.TextInput, referenceOnly: false, namespaceAliases, out var textInputElement))
            {
                rdfElement.Add(textInputElement);
            }

            foreach (var itemToFormat in feed.Channel.Items)
            {
                if (TryFormatRss10Item(itemToFormat, referenceOnly: false, namespaceAliases, out var itemElement))
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

        private static bool TryFormatRss10Channel(Rss10Channel channelToFormat, XNamespaceAliasSet namespaceAliases, out XElement channelElement)
        {
            channelElement = default;

            if (channelToFormat == null)
                return false;

            channelElement = new XElement(_rss + "channel");
            
            channelElement.Add(new XAttribute(_rdf + "about", channelToFormat.About));

            channelElement.Add(new XElement(_rss + "title", channelToFormat.Title));
            channelElement.Add(new XElement(_rss + "link", channelToFormat.Link));
            channelElement.Add(new XElement(_rss + "description", channelToFormat.Description));

            // extensions
            if (Rss10SyndicationChannelExtensionFormatter.TryFormatRss10SyndicationChannelExtension(channelToFormat.SyndicationExtension, namespaceAliases, out var syndicationExtensionElements))
            {
                channelElement.AddRange(syndicationExtensionElements);
            }

            if (DublinCoreElementExtensionFormatter.TryFormatDublinCoreElementExtension(channelToFormat.DublinCoreExtension, namespaceAliases, out var dublinCoreExtensionElements))
            {
                channelElement.AddRange(dublinCoreExtensionElements);
            }
            
            if (TryFormatRss10Image(channelToFormat.Image, referenceOnly: true, namespaceAliases, out var imageElement))
            {
                channelElement.Add(imageElement);
            }

            if (TryFormatRss10TextInput(channelToFormat.TextInput, referenceOnly: true, namespaceAliases, out var textInputElement))
            {
                channelElement.Add(textInputElement);
            }

            // items
            var liElements = new List<XElement>();
            foreach (var itemToFormat in channelToFormat.Items)
            {
                if (TryFormatRss10Item(itemToFormat, referenceOnly: true, namespaceAliases, out var liElement))
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

        private static bool TryFormatRss10Item(Rss10Item itemToFormat, bool referenceOnly, XNamespaceAliasSet namespaceAliases, out XElement itemElement)
        {
            itemElement = default;

            if (itemToFormat == null)
                return false;

            itemElement = referenceOnly
                ? new XElement(_rdf + "li", new XAttribute("resource", itemToFormat.About))
                : new XElement(_rss + "item", new XAttribute(_rdf + "about", itemToFormat.About));

            if (referenceOnly)
                return true;

            itemElement.Add(new XElement(_rss + "title") { Value = itemToFormat.Title });
            itemElement.Add(new XElement(_rss + "link") { Value = itemToFormat.Link });

            if (TryFormatOptionalTextElement(itemToFormat.Description, _rss + "description", out var descriptionElement))
            {
                itemElement.Add(descriptionElement);
            }

            // extensions
            if (Rss10ContentItemExtensionFormatter.TryFormatRss10ContentItemExtension(itemToFormat.ContentExtension, namespaceAliases, out var contentExtensionElements))
            {
                itemElement.AddRange(contentExtensionElements);
            }

            if (Rss10SlashItemExtensionFormatter.TryFormatRss10SlashItemExtension(itemToFormat.SlashExtension, namespaceAliases, out var slashExtensionElements))
            {
                itemElement.AddRange(slashExtensionElements);
            }

            if (WfwItemExtensionFormatter.TryFormatWfwItemExtension(itemToFormat.WfwExtension, namespaceAliases, out var wfwExtensionElements))
            {
                itemElement.AddRange(wfwExtensionElements);
            }

            if (DublinCoreElementExtensionFormatter.TryFormatDublinCoreElementExtension(itemToFormat.DublinCoreExtension, namespaceAliases, out var dublinCoreExtensionElements))
            {
                itemElement.AddRange(dublinCoreExtensionElements);
            }

            return true;
        }

        private static bool TryFormatRss10TextInput(Rss10TextInput textInputToFormat, bool referenceOnly, XNamespaceAliasSet namespaceAliases, out XElement textInputElement)
        {
            textInputElement = default;

            if (textInputToFormat == null)
                return false;

            textInputElement = referenceOnly
                ? new XElement(_rss + "textinput", new XAttribute(_rdf + "resource", textInputToFormat.About))
                : new XElement(_rss + "textinput", new XAttribute(_rdf + "about", textInputToFormat.About));

            if (referenceOnly)
                return true;

            textInputElement.Add(new XElement(_rss + "title") { Value = textInputToFormat.Title });
            textInputElement.Add(new XElement(_rss + "description") { Value = textInputToFormat.Description });
            textInputElement.Add(new XElement(_rss + "name") { Value = textInputToFormat.Name });
            textInputElement.Add(new XElement(_rss + "link") { Value = textInputToFormat.Link });

            // extensions
            if (DublinCoreElementExtensionFormatter.TryFormatDublinCoreElementExtension(textInputToFormat.DublinCoreExtension, namespaceAliases, out var dublinCoreExtensionElements))
            {
                textInputElement.AddRange(dublinCoreExtensionElements);
            }

            return true;
        }

        private static bool TryFormatRss10Image(Rss10Image imageToFormat, bool referenceOnly, XNamespaceAliasSet namespaceAliases, out XElement imageElement)
        {
            imageElement = default;

            if (imageToFormat == null)
                return false;

            imageElement = referenceOnly
                ? new XElement(_rss + "image", new XAttribute(_rdf + "resource", imageToFormat.About))
                : new XElement(_rss + "image", new XAttribute(_rdf + "about", imageToFormat.About));
            
            if (referenceOnly)
                return true;

            imageElement.Add(new XElement(_rss + "title") { Value = imageToFormat.Title });
            imageElement.Add(new XElement(_rss + "url") { Value = imageToFormat.Url });
            imageElement.Add(new XElement(_rss + "link") { Value = imageToFormat.Link });

            if (DublinCoreElementExtensionFormatter.TryFormatDublinCoreElementExtension(imageToFormat.DublinCoreExtension, namespaceAliases, out var dublinCoreExtensionElements))
            {
                imageElement.AddRange(dublinCoreExtensionElements);
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