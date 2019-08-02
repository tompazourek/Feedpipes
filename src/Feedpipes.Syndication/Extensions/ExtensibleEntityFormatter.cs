using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.CreativeCommons;
using Feedpipes.Syndication.Extensions.DublinCore;
using Feedpipes.Syndication.Extensions.Rss10Content;
using Feedpipes.Syndication.Extensions.Rss10Slash;
using Feedpipes.Syndication.Extensions.Rss10Syndication;
using Feedpipes.Syndication.Extensions.RssAtom10;
using Feedpipes.Syndication.Extensions.WellFormedWeb;
using Feedpipes.Syndication.Xml;

namespace Feedpipes.Syndication.Extensions
{
    internal static class ExtensibleEntityFormatter
    {
        [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
        public static bool TryFormatExtensibleEntity<T>(T entityToFormat, XNamespaceAliasSet namespaceAliases, out IList<XElement> elements)
            where T : class
        {
            elements = default;

            if (entityToFormat == null)
                return false;

            var results = new List<XElement>();

            if (entityToFormat is IExtensibleWithCreativeCommons extensibleWithCreativeCommons)
            {
                if (CreativeCommonsElementExtensionFormatter.TryFormatCreativeCommonsElementExtension(extensibleWithCreativeCommons.CreativeCommonsExtension, namespaceAliases, out var extensionElements))
                {
                    results.AddRange(extensionElements);
                }
            }

            if (entityToFormat is IExtensibleWithDublinCore extensibleWithDublinCore)
            {
                if (DublinCoreElementExtensionFormatter.TryFormatDublinCoreElementExtension(extensibleWithDublinCore.DublinCoreExtension, namespaceAliases, out var extensionElements))
                {
                    results.AddRange(extensionElements);
                }
            }

            if (entityToFormat is IExtensibleWithRss10Content extensibleWithRss10Content)
            {
                if (Rss10ContentItemExtensionFormatter.TryFormatRss10ContentItemExtension(extensibleWithRss10Content.ContentExtension, namespaceAliases, out var extensionElements))
                {
                    results.AddRange(extensionElements);
                }
            }

            if (entityToFormat is IExtensibleWithRss10Slash extensibleWithRss10Slash)
            {
                if (Rss10SlashItemExtensionFormatter.TryFormatRss10SlashItemExtension(extensibleWithRss10Slash.SlashExtension, namespaceAliases, out var extensionElements))
                {
                    results.AddRange(extensionElements);
                }
            }

            if (entityToFormat is IExtensibleWithRss10Syndication extensibleWithRss10Syndication)
            {
                if (Rss10SyndicationChannelExtensionFormatter.TryFormatRss10SyndicationChannelExtension(extensibleWithRss10Syndication.SyndicationExtension, namespaceAliases, out var extensionElements))
                {
                    results.AddRange(extensionElements);
                }
            }

            if (entityToFormat is IExtensibleWithRssAtom10 extensibleWithRssAtom10)
            {
                if (RssAtom10ElementExtensionFormatter.TryFormatRssAtom10ElementExtension(extensibleWithRssAtom10.AtomExtension, namespaceAliases, out var extensionElements))
                {
                    results.AddRange(extensionElements);
                }
            }

            if (entityToFormat is IExtensibleWithWfw extensibleWithWfw)
            {
                if (WfwItemExtensionFormatter.TryFormatWfwItemExtension(extensibleWithWfw.WfwExtension, namespaceAliases, out var extensionElements))
                {
                    results.AddRange(extensionElements);
                }
            }

            if (!results.Any())
                return false;

            elements = results;

            return true;
        }
    }
}