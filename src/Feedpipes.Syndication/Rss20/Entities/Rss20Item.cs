using System;
using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Syndication.Base;
using Feedpipes.Syndication.Extensions.CreativeCommons.Entities;
using Feedpipes.Syndication.Extensions.DublinCore.Entities;
using Feedpipes.Syndication.Extensions.Rss10Content.Entities;
using Feedpipes.Syndication.Extensions.Rss10Slash.Entities;
using Feedpipes.Syndication.Extensions.RssAtom10.Entities;
using Feedpipes.Syndication.Extensions.WellFormedWeb.Entities;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Rss20.Entities
{
    /// <summary>
    /// A channel may contain any number of itemss.
    /// An item may represent a "story" -- much like a story in a newspaper or magazine;
    /// if so its description is a synopsis of the story, and the link points to the full story.
    /// An item may also be complete in itself, if so, the description contains the text (entity-encoded HTML is allowed),
    /// and the link and title may be omitted. All elements of an item are optional, however at least one of
    /// title or description must be present.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss20Item : IRssItem, IFeedItem
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Title)
            .Append(x => x.Link)
            .Append(x => x.PubDate);

        /// <summary>
        /// Optional "title" element.
        /// The title of the item.
        /// </summary>
        /// <example>
        /// Venice Film Festival Tries to Quit Sinking
        /// </example>
        public string Title { get; set; }

        /// <summary>
        /// Optional "link" element.
        /// The URL of the item.
        /// </summary>
        /// <example>
        /// http://www.nytimes.com/2002/09/07/movies/07FEST.html
        /// </example>
        public string Link { get; set; }

        /// <summary>
        /// Optional "description" element.
        /// The item synopsis.
        /// </summary>
        /// <example>
        /// Some of the most heated chatter at the Venice Film Festival this week was about the way that the arrival of the stars
        /// at the Palazzo del Cinema was being staged.
        /// </example>
        public string Description { get; set; }

        /// <summary>
        /// Optional "author" element.
        /// Email address of the author of the item.
        /// It's the email address of the author of the item. For newspapers and magazines
        /// syndicating via RSS, the author is the person who wrote the article that the "item" describes.
        /// For collaborative weblogs, the author of the item might be different from the managing editor or webmaster.
        /// For a weblog authored by a single individual it would make sense to omit the "author" element.
        /// </summary>
        /// <example>
        /// lawyer@boyer.net (Lawyer Boyer)
        /// </example>
        public string Author { get; set; }

        /// <summary>
        /// Optional "comments" element.
        /// URL of a page for comments relating to the item.
        /// </summary>
        /// <example>
        /// http://www.myblog.org/cgi-local/mt/mt-comments.cgi?entry_id=290
        /// </example>
        public string Comments { get; set; }

        /// <summary>
        /// Optional "guid" element.
        /// A string that uniquely identifies the item.
        /// </summary>
        /// <example>
        /// http://inessential.com/2002/09/01.php#a2
        /// </example>
        public Rss20Guid Guid { get; set; }

        /// <summary>
        /// Optional "category" elements.
        /// Includes the item in one or more categories.
        /// </summary>
        /// <example>
        /// Simpsons Characters
        /// </example>
        public IList<Rss20Category> Categories { get; set; } = new List<Rss20Category>();

        /// <summary>
        /// Optional "enclosure" element.
        /// Describes a media object that is attached to the item.
        /// </summary>
        public Rss20Enclosure Enclosure { get; set; }

        /// <summary>
        /// Optional "pubDate" element.
        /// Indicates when the item was published.
        /// </summary>
        /// <example>
        /// Sun, 19 May 2002 15:21:36 GMT
        /// </example>
        public DateTimeOffset? PubDate { get; set; }

        /// <summary>
        /// Optional "source" element.
        /// The RSS channel that the item came from.
        /// </summary>
        public Rss20Source Source { get; set; }

        public Rss10ContentItemExtension ContentExtension { get; set; }
        public WfwItemExtension WfwExtension { get; set; }
        public Rss10SlashItemExtension SlashExtension { get; set; }
        public DublinCoreElementExtension DublinCoreExtension { get; set; }
        public CreativeCommonsElementExtension CreativeCommonsExtension { get; set; }

        public RssAtom10ElementExtension AtomExtension { get; set; }
    }
}