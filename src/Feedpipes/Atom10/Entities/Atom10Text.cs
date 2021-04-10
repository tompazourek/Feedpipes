using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Atom10.Entities
{
    /// <summary>
    /// "title", "summary", "content", and "rights" contain human-readable text, usually in small quantities. The type
    /// attribute determines how this information is encoded (default="text")
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Atom10Text
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Type)
            .Append(x => x.Value)
            .Append(x => x.Lang)
            .Append(x => x.Base);

        /// <summary>
        /// Corresponds to the "type" attribute.
        /// By default, "text", "html"/"xhtml" otherwise.
        /// </summary>
        public string Type { get; set; } = "text";

        /// <summary>
        /// Value of the element.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Corresponds to the "xml:lang" attribute.
        /// xml:lang may be used to identify the language of any human readable text.
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Corresponds to the "xml:base" attribute.
        /// xml:base may be used to control how relative URIs are resolved.
        /// </summary>
        public string Base { get; set; }
    }
}
