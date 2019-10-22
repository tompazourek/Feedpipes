using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.Rss10Slash.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss10SlashExtension : IExtensionEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Comments)
            .Append(x => x.Section)
            .Append(x => x.Department)
            .Append(x => x.HitParade);

        /// <summary>
        /// Corresponds to "slash:comments" element.
        /// Represents the number of comments.
        /// </summary>
        public int? Comments { get; set; }

        /// <summary>
        /// Corresponds to "slash:section" element.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Corresponds to "slash:section" element.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Corresponds to "slash:hit_parade" element.
        /// </summary>
        public IList<int> HitParade { get; set; } = new List<int>();
    }
}