using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.Rss10Content.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss10ContentExtension : IExtensionEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Encoded);

        /// <summary>
        /// Corresponds to "content:encoded" element.
        /// An element whose contents are the entity-encoded or CDATA-escaped version of the content of the item.
        /// </summary>
        public string Encoded { get; set; }
    }
}
