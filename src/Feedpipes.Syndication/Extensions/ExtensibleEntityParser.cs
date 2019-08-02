using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;
using Feedpipes.Syndication.Extensions.CreativeCommons;
using Feedpipes.Syndication.Extensions.DublinCore;
using Feedpipes.Syndication.Extensions.Rss10Content;
using Feedpipes.Syndication.Extensions.Rss10Slash;
using Feedpipes.Syndication.Extensions.Rss10Syndication;
using Feedpipes.Syndication.Extensions.RssAtom10;
using Feedpipes.Syndication.Extensions.WellFormedWeb;

namespace Feedpipes.Syndication.Extensions
{
    internal static class ExtensibleEntityParser
    {
        [SuppressMessage("ReSharper", "ConstantNullCoalescingCondition")]
        [SuppressMessage("ReSharper", "InvertIf")]
        public static void ParseExtensibleEntity<T>(XElement parentElement, T entityToExtend)
        {
            if (entityToExtend is IExtensibleWithCreativeCommons extensibleWithCreativeCommons)
            {
                if (CreativeCommonsElementExtensionParser.TryParseCreativeCommonsElementExtension(parentElement, out var creativeCommonsExtension))
                {
                    extensibleWithCreativeCommons.CreativeCommonsExtension = creativeCommonsExtension;
                }
            }

            if (entityToExtend is IExtensibleWithDublinCore extensibleWithDublinCore)
            {
                if (DublinCoreElementExtensionParser.TryParseDublinCoreElementExtension(parentElement, out var dublinCoreExtension))
                {
                    extensibleWithDublinCore.DublinCoreExtension = dublinCoreExtension;
                }
            }

            if (entityToExtend is IExtensibleWithRss10Content extensibleWithRss10Content)
            {
                if (Rss10ContentItemExtensionParser.TryParseRss10ContentItemExtension(parentElement, out var contentExtension))
                {
                    extensibleWithRss10Content.ContentExtension = contentExtension;
                }
            }

            if (entityToExtend is IExtensibleWithRss10Slash extensibleWithRss10Slash)
            {
                if (Rss10SlashItemExtensionParser.TryParseRss10SlashItemExtension(parentElement, out var slashExtension))
                {
                    extensibleWithRss10Slash.SlashExtension = slashExtension;
                }
            }

            if (entityToExtend is IExtensibleWithRss10Syndication extensibleWithRss10Syndication)
            {
                if (Rss10SyndicationChannelExtensionParser.TryParseRss10SyndicationChannelExtension(parentElement, out var syndicationExtension))
                {
                    extensibleWithRss10Syndication.SyndicationExtension = syndicationExtension;
                }
            }

            if (entityToExtend is IExtensibleWithRssAtom10 extensibleWithRssAtom10)
            {
                if (RssAtom10ElementExtensionParser.TryParseRssAtom10ElementExtension(parentElement, out var atomExtension))
                {
                    extensibleWithRssAtom10.AtomExtension = atomExtension;
                }
            }

            if (entityToExtend is IExtensibleWithWfw extensibleWithWfw)
            {
                if (WfwItemExtensionParser.TryParseWfwItemExtension(parentElement, out var wfwExtension))
                {
                    extensibleWithWfw.WfwExtension = wfwExtension;
                }
            }
        }
    }
}