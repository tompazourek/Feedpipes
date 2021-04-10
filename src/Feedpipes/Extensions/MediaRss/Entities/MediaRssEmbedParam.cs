using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Allows inclusion of such information in the form of key-value pairs.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssEmbedParam
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Name)
            .Append(x => x.Value);

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
