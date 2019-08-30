using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Extensions.MediaRss.Entities
{
    /// <summary>
    /// statistics This element specifies various statistics about a media object like the view count and the favorite count.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssCommunityStatistics
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Views)
            .Append(x => x.Favorites);

        public int? Views { get; set; }
        public int? Favorites { get; set; }
    }
}