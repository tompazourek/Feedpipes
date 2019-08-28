using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Syndication.Extensions;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.JsonFeedFormat.Entities
{
    /// <summary>
    /// JSON Feed
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class JsonFeed : IExtensibleEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Title)
            .Append(x => x.HomePageUrl)
            .Append(x => x.FeedUrl)
            .Append(x => x.Items);

        /// <summary>
        /// title (required, string) is the name of the feed, which will often correspond
        /// to the name of the website (blog, for instance), though not necessarily.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// home_page_url (optional but strongly recommended, string) is the URL of the resource that
        /// the feed describes. This resource may or may not actually be a “home” page, but it should
        /// be an HTML page. If a feed is published on the public web, this should be considered as required.
        /// But it may not make sense in the case of a file created on a desktop computer, when that file
        /// is not shared or is shared only privately.
        /// </summary>
        public string HomePageUrl { get; set; }

        /// <summary>
        /// feed_url (optional but strongly recommended, string) is the URL of the feed, and serves as the unique
        /// identifier for the feed. As with  home_page_url, this should be considered required for feeds on the public web.
        /// </summary>
        public string FeedUrl { get; set; }

        /// <summary>
        /// description (optional, string) provides more detail, beyond the title, on what the feed is about.
        /// A feed reader may display this text.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// user_comment (optional, string) is a description of the purpose of the feed. This is for the use of people
        /// looking at the raw JSON, and should be ignored by feed readers.
        /// </summary>
        public string UserComment { get; set; }

        /// <summary>
        /// next_url (optional, string) is the URL of a feed that provides the next n items, where n is determined by the
        /// publisher.
        /// This allows for pagination, but with the expectation that reader software is not required to use it and probably won’t
        /// use it very often. next_url must not be the same as feed_url, and it must not be the same as a previous  next_url
        /// (to avoid infinite loops).
        /// </summary>
        public string NextUrl { get; set; }

        /// <summary>
        /// icon (optional, string) is the URL of an image for the feed suitable to be used in a timeline, much the way an avatar
        /// might be used. It should be square and relatively large — such as 512 x 512 — so that it can be scaled-down and so that
        /// it can look good on retina displays. It should use transparency where appropriate, since it may be rendered
        /// on a non-white background.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// favicon (optional, string) is the URL of an image for the feed suitable to be used in a source list.
        /// It should be square and relatively small, but not smaller than 64 x 64 (so that it can look good on retina displays).
        /// As with icon, this image should use transparency where appropriate, since it may be rendered on a non-white background.
        /// </summary>
        public string Favicon { get; set; }

        /// <summary>
        /// author (optional, object) specifies the feed author. The author object has several members.
        /// </summary>
        public JsonFeedAuthor Author { get; set; }

        /// <summary>
        /// expired (optional, boolean) says whether or not the feed is finished — that is, whether or not it will
        /// ever update again. A feed for a temporary event, such as an instance of the Olympics, could expire.
        /// If the value is  true, then it’s expired. Any other value, or the absence of expired, means the feed
        /// may continue to update.
        /// </summary>
        public bool? Expired { get; set; }

        /// <summary>
        /// hubs (very optional, array of objects) describes endpoints that can be used to subscribe to real-time notifications
        /// from the publisher of this feed.
        /// </summary>
        public IList<JsonFeedHub> Hubs { get; set; } = new List<JsonFeedHub>();

        /// <summary>
        /// items is an array, and is required.
        /// </summary>
        public IList<JsonFeedItem> Items { get; set; } = new List<JsonFeedItem>();
        
        /// <summary>
        /// Extenssions
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}