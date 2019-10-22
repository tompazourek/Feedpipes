using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Text value that can be either HTML or plaintext.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssTypedText
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Type)
            .Append(x => x.Value);

        /// <summary>
        /// type specifies the type of text embedded. Possible values are either "plain" or "html".
        /// Default value is "plain".
        /// All HTML must be entity-encoded.
        /// It is an optional attribute.
        /// </summary>
        public string Type { get; set; } = "plain";

        public string Value { get; set; }
    }
}