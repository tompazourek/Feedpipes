using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Rss10.Entities
{
    /// <summary>
    /// Root RSS 1.0 RDF element. Corresponds to "rss" element.
    /// </summary>
    /// <remarks>
    /// Based on: http://web.resource.org/rss/1.0/spec
    /// </remarks>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss10Feed
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Channel, x => x.DebuggerDisplay);

        public Rss10Channel Channel { get; set; }
    }
}
