using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Atom10.Entities
{
    /// <summary>
    /// Corresponds to the "generator" element.
    /// Identifies the software used to generate the feed, for debugging and other purposes.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Atom10Generator
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Value)
            .Append(x => x.Version)
            .Append(x => x.Uri);

        /// <summary>
        /// Optional "uri" attribute.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Optional "version" attribute.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Contents of the "generator" element.
        /// </summary>
        public string Value { get; set; }
    }
}
