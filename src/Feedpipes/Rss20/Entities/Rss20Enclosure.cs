using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Rss20.Entities
{
    /// <summary>
    /// Describes a media object that is attached to the item.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss20Enclosure
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Url)
            .Append(x => x.Length)
            .Append(x => x.Type);

        /// <summary>
        /// URL says where the enclosure is located.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Length says how big it is in bytes.
        /// </summary>
        public string Length { get; set; }

        /// <summary>
        /// Type says what its type is, a standard MIME type.
        /// </summary>
        public string Type { get; set; }
    }
}
