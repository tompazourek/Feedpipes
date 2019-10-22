using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.JsonFeedFormat.Entities
{
    /// <summary>
    /// author (optional, object) specifies the feed author. The author object has several members.
    /// These are all optional — but if you provide an author object, then at least one is required.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class JsonFeedAuthor
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Name)
            .Append(x => x.Url)
            .Append(x => x.Avatar);

        /// <summary>
        /// name (optional, string) is the author’s name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// url (optional, string) is the URL of a site owned by the author. It could be a blog, micro-blog,
        /// Twitter account, and so on. Ideally the linked-to page provides a way to contact the author,
        /// but that’s not required. The URL could be a mailto: link, though we suspect that will be rare.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// avatar (optional, string) is the URL for an image for the author. As with icon, it should be square
        /// and relatively large — such as 512 x 512 — and should use transparency where appropriate, since it may
        /// be rendered on a non-white background.
        /// </summary>
        public string Avatar { get; set; }
    }
}