using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.JsonFeedFormat.Entities
{
    /// <summary>
    /// hubs (very optional, array of objects) describes endpoints that can be used to subscribe to real-time notifications
    /// from the publisher of this feed. Each object has a type and url, both of which are required.
    /// See the section “Subscribing to Real-time Notifications” below for details.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class JsonFeedHub
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Type)
            .Append(x => x.Url);

        public string Type { get; set; }

        public string Url { get; set; }
    }
}