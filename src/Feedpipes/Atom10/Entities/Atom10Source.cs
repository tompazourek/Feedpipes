using System;
using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Atom10.Entities
{
    /// <summary>
    /// Corresponds to the "source" element.
    /// Contains metadata from the source feed if this entry is a copy.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Atom10Source
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Id)
            .Append(x => x.Title, x => x.DebuggerDisplay)
            .Append(x => x.Updated);

        /// <summary>
        /// Corresponds to the "id" element.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Corresponds to the "title" element.
        /// </summary>
        public Atom10Text Title { get; set; }

        /// <summary>
        /// Corresponds to the "updated" element.
        /// </summary>
        public DateTimeOffset? Updated { get; set; }
    }
}