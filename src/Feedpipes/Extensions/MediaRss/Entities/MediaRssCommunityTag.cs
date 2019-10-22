using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// This element contains user-generated tags separated by commas in the decreasing order of each tag's weight.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssCommunityTag
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Tag)
            .Append(x => x.Weight);

        public string Tag { get; set; }

        /// <summary>
        /// It's up to the provider to choose the way weight is determined for a tag; for example, number of occurences can
        /// be one way to decide weight of a particular tag. Default weight is 1.
        /// </summary>
        public double? Weight { get; set; }
    }
}