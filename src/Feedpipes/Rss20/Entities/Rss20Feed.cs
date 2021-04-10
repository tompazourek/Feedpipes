using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Rss20.Entities
{
    /// <summary>
    /// Root RSS 2.0 element. Corresponds to "rss" element.
    /// </summary>
    /// <remarks>
    /// Based on: https://validator.w3.org/feed/docs/rss2.html
    /// </remarks>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss20Feed
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Channel, x => x.DebuggerDisplay);

        public Rss20Channel Channel { get; set; }
    }
}
