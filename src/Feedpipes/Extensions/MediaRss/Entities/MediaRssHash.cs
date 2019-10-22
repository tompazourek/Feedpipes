using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// This is the hash of the binary media file. It can appear multiple times as long as each instance is a different algo.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssHash
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Algo)
            .Append(x => x.Value);

        /// <summary>
        /// algo indicates the algorithm used to create the hash.
        /// Possible values are "md5" and "sha-1".
        /// Default value is "md5".
        /// It is an optional attribute.
        /// </summary>
        public MediaRssHashAlgo Algo { get; set; }

        public string Value { get; set; }
    }
}