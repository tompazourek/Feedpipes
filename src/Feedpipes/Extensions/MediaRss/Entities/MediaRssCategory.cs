using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Allows a taxonomy to be set that gives an indication of the type of media content, and its particular contents.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssCategory
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Scheme)
            .Append(x => x.Label)
            .Append(x => x.Value);

        /// <summary>
        /// scheme is the URI that identifies the categorization scheme.
        /// It is an optional attribute.
        /// If this attribute is not included, the default scheme is "http://search.yahoo.com/mrss/category_schema".
        /// </summary>
        public string Scheme { get; set; } = "http://search.yahoo.com/mrss/category_schema";

        /// <summary>
        /// label is the human readable label that can be displayed in end user applications.
        /// It is an optional attribute.
        /// </summary>
        public string Label { get; set; }

        public string Value { get; set; }
    }
}