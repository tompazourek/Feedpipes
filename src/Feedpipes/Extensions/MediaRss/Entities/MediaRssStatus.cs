using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Specifies the status of a media object -- whether it's still active or it has been blocked/deleted.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssStatus
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.State)
            .Append(x => x.Reason);

        public MediaRssStatusState? State { get; set; }

        /// <summary>
        /// reason is a reason explaining why a media object has been blocked/deleted. It can be plain text or a URL.
        /// </summary>
        public string Reason { get; set; }
    }
}
