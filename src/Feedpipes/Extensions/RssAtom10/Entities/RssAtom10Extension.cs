using System;
using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Atom10.Entities;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.RssAtom10.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class RssAtom10Extension : IExtensionEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Id)
            .Append(x => x.Updated)
            .Append(x => x.Links);

        /// <summary>
        /// Optional "id" element.
        /// Identifies the feed using a universally unique and permanent URI. If you have a long-term,
        /// renewable lease on your Internet domain name, then you can feel free to use your website's address.
        /// </summary>
        /// <example>
        /// http://example.com/
        /// </example>
        public string Id { get; set; }

        /// <summary>
        /// Optional "updated" element.
        /// Indicates the last time the feed was modified in a significant way.
        /// </summary>
        /// <example>
        /// 2003-12-13T18:30:02Z
        /// </example>
        public DateTimeOffset? Updated { get; set; }

        /// <summary>
        /// Corresponds to the "link" elements.
        /// Identifies a related Web page. The type of relation is defined by the rel attribute.
        /// A feed is limited to one alternate per type and hreflang.
        /// A feed should contain a link back to the feed itself.
        /// </summary>
        public IList<Atom10Link> Links { get; set; } = new List<Atom10Link>();
    }
}
