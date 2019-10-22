using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// This element stands for the community related content. This allows inclusion of the user perception about a media
    /// object
    /// in the form of view count, ratings and tags.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssCommunity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.StarRating, x => x.DebuggerDisplay)
            .Append(x => x.Statistics, x => x.DebuggerDisplay)
            .Append(x => x.Tags);

        /// <summary>
        /// starRating This element specifies the rating-related information about a media object.
        /// </summary>
        public MediaRssCommunityStarRating StarRating { get; set; }

        /// <summary>
        /// statistics This element specifies various statistics about a media object like the view count and the favorite count.
        /// </summary>
        public MediaRssCommunityStatistics Statistics { get; set; }

        /// <summary>
        /// Contains user-generated tags separated by commas in the decreasing order of each tag's weight.
        /// </summary>
        public IList<MediaRssCommunityTag> Tags { get; set; } = new List<MediaRssCommunityTag>();
    }
}