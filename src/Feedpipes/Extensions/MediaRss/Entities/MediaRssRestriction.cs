using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Allows restrictions to be placed on the aggregator rendering the media in the feed. Currently, restrictions are
    /// based on distributor (URI), country codes and sharing of a media object. This element is purely informational and
    /// no obligation can be assumed or implied.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssRestriction
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Values)
            .Append(x => x.Relationship)
            .Append(x => x.Type);

        /// <remarks>
        /// To allow the producer to explicitly declare his/her intentions, two literals are reserved: "all", "none".
        /// </remarks>
        public IList<string> Values { get; set; } = new List<string>();

        /// <summary>
        /// relationship indicates the type of relationship that the restriction represents (allow | deny).
        /// It is a required attribute.
        /// </summary>
        public MediaRssRestrictionRelationship? Relationship { get; set; }

        /// <summary>
        /// type specifies the type of restriction (country | uri | sharing ) that the media can be syndicated.
        /// It is an optional attribute; however can only be excluded when using one of the literal values "all" or "none".
        /// </summary>
        public MediaRssRestrictionType? Type { get; set; }
    }
}
