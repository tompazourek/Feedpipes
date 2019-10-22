using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Sometimes player-specific embed code is needed for a player to play any video.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssEmbed
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Url)
            .Append(x => x.Height)
            .Append(x => x.Width)
            .Append(x => x.Params);

        public string Url { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }

        /// <summary>
        /// Allows inclusion of such information in the form of key-value pairs.
        /// </summary>
        public IList<MediaRssEmbedParam> Params { get; set; } = new List<MediaRssEmbedParam>();
    }
}