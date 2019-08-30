using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Copyright information for the media object.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssCopyright
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Url)
            .Append(x => x.Value);

        /// <summary>
        /// url is the URL for a terms of use page or additional copyright information.
        /// If the media is operating under a Creative Commons license, the Creative Commons module should be used instead.
        /// It is an optional attribute.
        /// </summary>
        public string Url { get; set; }

        public string Value { get; set; }
    }
}