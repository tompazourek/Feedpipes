using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Extensions.MediaRss.Entities
{
    /// <summary>
    /// starRating This element specifies the rating-related information about a media object.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssCommunityStarRating
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Average)
            .Append(x => x.Count)
            .Append(x => x.Min)
            .Append(x => x.Max);

        public double? Average { get; set; }
        public int? Count { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }
    }
}