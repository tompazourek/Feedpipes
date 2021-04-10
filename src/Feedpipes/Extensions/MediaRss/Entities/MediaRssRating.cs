using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// This allows the permissible audience to be declared. If this element is not included, it assumes that
    /// no restrictions are necessary.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssRating
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Scheme)
            .Append(x => x.Value);

        /// <summary>
        /// scheme is the URI that identifies the rating scheme.
        /// It is an optional attribute.
        /// If this attribute is not included, the default scheme is urn:simple (adult | nonadult).
        /// </summary>
        public string Scheme { get; set; } = "urn:simple";

        public string Value { get; set; }
    }
}
