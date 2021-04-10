using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Rss20.Entities
{
    /// <summary>
    /// Allows processes to register with a cloud to be notified of updates to the channel, implementing a lightweight
    /// publish-subscribe protocol for RSS feeds.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss20Cloud
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Domain)
            .Append(x => x.Port)
            .Append(x => x.Path)
            .Append(x => x.RegisterProcedure)
            .Append(x => x.Protocol);

        public string Domain { get; set; }
        public string Port { get; set; }
        public string Path { get; set; }
        public string RegisterProcedure { get; set; }
        public string Protocol { get; set; }
    }
}
