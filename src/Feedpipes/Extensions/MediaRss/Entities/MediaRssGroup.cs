using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// "media:group" is a sub-element of "item". It allows grouping of "media:content" elements
    /// that are effectively the same content, yet different representations. For instance: the same song recorded
    /// in both the WAV and MP3 format. It's an optional element that must only be used for this purpose.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssGroup : MediaRssContainer, IExtensibleEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Contents);

        public IList<MediaRssContent> Contents { get; set; } = new List<MediaRssContent>();

        /// <summary>
        /// Extensions (for Dublin Core and other)
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}