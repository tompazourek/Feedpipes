using System;
using System.Collections.Generic;
using Feedpipes.Syndication.Extensions.DublinCore.Entities;
using Feedpipes.Syndication.Extensions.Rss10Syndication.Entities;

namespace Feedpipes.Syndication.Rss20.Entities
{
    public class Rss20Channel
    {
        /// <summary>
        /// Required "title" element.
        /// The name of the channel. It's how people refer to your service. If you have an HTML website that contains
        /// the same information as your RSS file, the title of your channel should be the same as the title of your website.
        /// </summary>
        /// <example>GoUpstate.com News Headlines</example>
        public string Title { get; set; }

        /// <summary>
        /// Required "link" element.
        /// The URL to the HTML website corresponding to the channel.
        /// </summary>
        /// <example>
        /// http://www.goupstate.com/
        /// </example>
        public string Link { get; set; }

        /// <summary>
        /// Required "description" element.
        /// Phrase or sentence describing the channel.
        /// </summary>
        /// <example>
        /// The latest news from GoUpstate.com, a Spartanburg Herald-Journal Web site.
        /// </example>
        public string Description { get; set; }

        /// <summary>
        /// Optional "language" element.
        /// The language the channel is written in. This allows aggregators to group all Italian language sites,
        /// for example, on a single page. A list of allowable values for this element, as provided by Netscape, is here.
        /// You may also use values defined by the W3C.
        /// </summary>
        /// <example>
        /// en-us
        /// </example>
        public string Language { get; set; }

        /// <summary>
        /// Optional "copyright" element.
        /// Copyright notice for content in the channel.
        /// </summary>
        /// <example>
        /// Copyright 2002, Spartanburg Herald-Journal
        /// </example>
        public string Copyright { get; set; }

        /// <summary>
        /// Optional "managingEditor" element.
        /// Email address for person responsible for editorial content.
        /// </summary>
        /// <example>
        /// geo@herald.com (George Matesky)
        /// </example>
        public string ManagingEditor { get; set; }

        /// <summary>
        /// Optional "webMaster" element.
        /// Email address for person responsible for technical issues relating to channel.
        /// </summary>
        /// <example>
        /// betty@herald.com (Betty Guernsey)
        /// </example>
        public string WebMaster { get; set; }

        /// <summary>
        /// Optional "pubDate" element.
        /// The publication date for the content in the channel. For example, the New York Times publishes on a daily basis,
        /// the publication date flips once every 24 hours. That's when the pubDate of the channel changes.
        /// All date-times in RSS conform to the Date and Time Specification of RFC 822, with the exception that the year
        /// may be expressed with two characters or four characters (four preferred).
        /// </summary>
        /// <example>
        /// Sat, 07 Sep 2002 0:00:01 GMT
        /// </example>
        public DateTimeOffset? PubDate { get; set; }

        /// <summary>
        /// Optional "lastBuildDate" element.
        /// The last time the content of the channel changed.
        /// </summary>
        /// <example>
        /// Sat, 07 Sep 2002 9:42:31 GMT
        /// </example>
        public DateTimeOffset? LastBuildDate { get; set; }

        /// <summary>
        /// Optional "category" elements.
        /// Specify one or more categories that the channel belongs to.
        /// Follows the same rules as the item-level category element.
        /// </summary>
        public IList<Rss20Category> Categories { get; set; } = new List<Rss20Category>();

        /// <summary>
        /// Optional "generator" element.
        /// A string indicating the program used to generate the channel.
        /// </summary>
        /// <example>
        /// MightyInHouse Content System v2.3
        /// </example>
        public string Generator { get; set; }

        /// <summary>
        /// Optional "docs" element.
        /// A URL that points to the documentation for the format used in the RSS file.
        /// It's probably a pointer to this page. It's for people who might stumble across an RSS file
        /// on a Web server 25 years from now and wonder what it is.
        /// </summary>
        /// <example>
        /// http://backend.userland.com/rss
        /// </example>
        public string Docs { get; set; }

        /// <summary>
        /// Optional "cloud" element.
        /// Allows processes to register with a cloud to be notified of updates to the channel, implementing a lightweight
        /// publish-subscribe protocol for RSS feeds.
        /// </summary>
        public Rss20Cloud Cloud { get; set; }

        /// <summary>
        /// Optional "ttl" element.
        /// ttl stands for time to live. It's a number of minutes that indicates how long a channel can be cached
        /// before refreshing from the source. More info here.
        /// </summary>
        public TimeSpan? Ttl { get; set; }

        /// <summary>
        /// Optional "image" element.
        /// Specifies a GIF, JPEG or PNG image that can be displayed with the channel.
        /// </summary>
        public Rss20Image Image { get; set; }

        /// <summary>
        /// Optional "textInput" element.
        /// Specifies a text input box that can be displayed with the channel.
        /// </summary>
        public Rss20TextInput TextInput { get; set; }

        /// <summary>
        /// Optional "skipHours" element.
        /// A hint for aggregators telling them which hours they can skip.
        /// Up to 24 numbers ranging between 0 and 23 representing a time in GMT, when aggregators, if they support the feature,
        /// may not read the channel.
        /// </summary>
        public IList<int> SkipHours { get; set; } = new List<int>();

        /// <summary>
        /// Optional "skipDays" element.
        /// A hint for aggregators telling them which days of week they can skip.
        /// </summary>
        public IList<DayOfWeek> SkipDays { get; set; } = new List<DayOfWeek>();

        /// <summary>
        /// List of "item" elements.
        /// </summary>
        public IList<Rss20Item> Items { get; set; } = new List<Rss20Item>();

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