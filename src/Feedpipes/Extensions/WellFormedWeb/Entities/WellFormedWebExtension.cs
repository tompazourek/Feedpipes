using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.WellFormedWeb.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class WellFormedWebExtension : IExtensionEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Comment)
            .Append(x => x.CommentRss);

        /// <summary>
        /// Corresponds to "wfw:comment".
        /// This element appears in RSS feeds and contains the URI that comment entries are to be POSTed to.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Corresponds to "wfw:commentRss".
        /// The second element to appear in the wfw namespace is commentRss. This element also appears in RSS feeds and contains
        /// the URI of the RSS feed for comments on that Item.
        /// </summary>
        public string CommentRss { get; set; }
    }
}
