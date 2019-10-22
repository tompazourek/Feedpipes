using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Atom10.Entities
{
    /// <summary>
    /// Corresponds to the "category" element.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Atom10Category
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Term)
            .Append(x => x.Scheme)
            .Append(x => x.Label);

        /// <summary>
        /// Required "term" attribute.
        /// term identifies the category.
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// Optional "scheme" attribute.
        /// scheme identifies the categorization scheme via a URI.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Optional "label" attribute.
        /// label provides a human-readable label for display.
        /// </summary>
        public string Label { get; set; }
    }
}