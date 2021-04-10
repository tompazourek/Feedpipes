using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssExtension : MediaRssContainer, IExtensionEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Groups)
            .Append(x => x.Contents);

        public IList<MediaRssGroup> Groups { get; set; } = new List<MediaRssGroup>();

        public IList<MediaRssContent> Contents { get; set; } = new List<MediaRssContent>();
    }
}
