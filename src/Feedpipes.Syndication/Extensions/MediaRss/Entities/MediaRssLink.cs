using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Reused element for various links.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssLink
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Type)
            .Append(x => x.Lang)
            .Append(x => x.Href);

        public string Type { get; set; }
        public string Lang { get; set; }
        public string Href { get; set; }
    }
}