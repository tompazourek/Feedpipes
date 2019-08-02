using System;
using System.Collections.Generic;
using Feedpipes.Syndication.Extensions.DublinCore.Entities;
using Feedpipes.Syndication.Extensions.Rss10Syndication.Entities;

namespace Feedpipes.Syndication.Atom10.Entities
{
    public class Atom10Feed
    {
        /// <summary>
        /// Corresponds to the "xml:lang" attribute.
        /// xml:lang may be used to identify the language of any human readable text.
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Corresponds to the "xml:base" attribute.
        /// xml:base may be used to control how relative URIs are resolved.
        /// </summary>
        public string Base { get; set; }

        /// <summary>
        /// Required "id" element.
        /// Identifies the feed using a universally unique and permanent URI. If you have a long-term,
        /// renewable lease on your Internet domain name, then you can feel free to use your website's address.
        /// </summary>
        /// <example>
        /// http://example.com/
        /// </example>
        public string Id { get; set; }

        /// <summary>
        /// Required "title" element.
        /// Contains a human readable title for the feed. Often the same as the title of the associated website.
        /// This value should not be blank.
        /// </summary>
        /// <example>
        /// Example, Inc.
        /// </example>
        public Atom10Text Title { get; set; }

        /// <summary>
        /// Required "updated" element.
        /// Indicates the last time the feed was modified in a significant way.
        /// </summary>
        /// <example>
        /// 2003-12-13T18:30:02Z
        /// </example>
        public DateTimeOffset? Updated { get; set; }

        /// <summary>
        /// Corresponds to the "author" elements (Recommended).
        /// Names one author of the feed. A feed may have multiple author elements.
        /// A feed must contain at least one author element unless all of the entry elements contain at
        /// least one author element.
        /// </summary>
        public IList<Atom10Person> Authors { get; set; } = new List<Atom10Person>();

        /// <summary>
        /// Corresponds to the "link" elements (Recommended).
        /// Identifies a related Web page. The type of relation is defined by the rel attribute.
        /// A feed is limited to one alternate per type and hreflang.
        /// A feed should contain a link back to the feed itself.
        /// </summary>
        public IList<Atom10Link> Links { get; set; } = new List<Atom10Link>();

        /// <summary>
        /// Corresponds to the optional "category" elements.
        /// Specifies a category that the feed belongs to. A feed may have multiple category elements.
        /// </summary>
        public IList<Atom10Category> Categories { get; set; } = new List<Atom10Category>();

        /// <summary>
        /// Corresponds to the optional "contributor" elements.
        /// Names one contributor to the feed. A feed may have multiple contributor elements.
        /// </summary>
        public IList<Atom10Person> Contributors { get; set; } = new List<Atom10Person>();

        /// <summary>
        /// Optional "generator" element.
        /// Identifies the software used to generate the feed, for debugging and other purposes.
        /// </summary>
        public Atom10Generator Generator { get; set; }

        /// <summary>
        /// Optional "icon" element.
        /// Identifies a small image which provides iconic visual identification for the feed. Icons should be square.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Optional "logo" element.
        /// Identifies a larger image which provides visual identification for the feed.
        /// Images should be twice as wide as they are tall.
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// Optional "rights" element.
        /// Conveys information about rights, e.g. copyrights, held in and over the feed.
        /// </summary>
        /// <example>
        /// © 2005 John Doe
        /// </example>
        public Atom10Text Rights { get; set; }

        /// <summary>
        /// Optional "subtitle" element.
        /// Contains a human-readable description or subtitle for the feed.
        /// </summary>
        /// <example>
        /// all your examples are belong to us
        /// </example>
        public Atom10Text Subtitle { get; set; }

        /// <summary>
        /// Corresponds to the "entry" elements.
        /// An example of an entry would be a single post on a weblog.
        /// </summary>
        public IList<Atom10Entry> Entries { get; set; } = new List<Atom10Entry>();
        
        /// <summary>
        /// Optional "sy:*" extended information.
        /// </summary>
        public Rss10SyndicationChannelExtension SyndicationExtension { get; set; }

        /// <summary>
        /// Optional "dc:*" extended information.
        /// </summary>
        public DublinCoreElementExtension DublinCoreExtension { get; set; }
    }
}