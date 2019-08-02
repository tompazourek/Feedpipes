﻿using Feedpipes.Syndication.Base;
using Feedpipes.Syndication.Extensions.CreativeCommons.Entities;
using Feedpipes.Syndication.Extensions.DublinCore.Entities;
using Feedpipes.Syndication.Extensions.Rss10Content.Entities;
using Feedpipes.Syndication.Extensions.Rss10Slash.Entities;
using Feedpipes.Syndication.Extensions.RssAtom10.Entities;
using Feedpipes.Syndication.Extensions.WellFormedWeb.Entities;

namespace Feedpipes.Syndication.Rss10.Entities
{
    /// <summary>
    /// While commonly a news headline, with RSS 1.0's modular extensibility, this can be just about anything:
    /// discussion posting, job listing, software patch -- any object with a URI. There may be a minimum of one item
    /// per RSS document. While RSS 1.0 does not enforce an upper limit, for backward compatibility with RSS 0.9 and 0.91,
    /// a maximum of fifteen items is recommended.
    /// {item_uri} must be unique with respect to any other rdf:about attributes in the RSS document and is a URI
    /// which identifies the item. {item_uri} should be identical to the value of the "link" sub-element of
    /// the "item" element, if possible.
    /// </summary>
    public class Rss10Item : IRssItem, IFeedItem
    {
        /// <summary>
        /// Required "rdf:about" attribute.
        /// Usually the same as the link.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Required "title" element.
        /// The item's title.
        /// Suggested maximum length of 100 characters.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Required "link" element.
        /// The item's URL.
        /// Suggested maximum length of 500 characters.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Optional "description" element.
        /// A brief description/abstract of the item.
        /// Suggested maximum length of 500 characters.
        /// </summary>
        public string Description { get; set; }

        public Rss10ContentItemExtension ContentExtension { get; set; }
        public WfwItemExtension WfwExtension { get; set; }
        public Rss10SlashItemExtension SlashExtension { get; set; }
        public DublinCoreElementExtension DublinCoreExtension { get; set; }
        public CreativeCommonsElementExtension CreativeCommonsExtension { get; set; }

        public RssAtom10ElementExtension AtomExtension { get; set; }
    }
}