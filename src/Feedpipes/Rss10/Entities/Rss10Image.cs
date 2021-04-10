using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Extensions;
using Feedpipes.Utils;

namespace Feedpipes.Rss10.Entities
{
    /// <summary>
    /// An image to be associated with an HTML rendering of the channel. This image should be of a format
    /// supported by the majority of Web browsers. While the later 0.91 specification allowed for a width
    /// of 1-144 and height of 1-400, convention (and the 0.9 specification) dictate 88x31.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss10Image : IExtensibleEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Url)
            .Append(x => x.Title)
            .Append(x => x.Link);

        /// <summary>
        /// Required "rdf:about" attribute.
        /// It's usually the same as the URL.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Required "title" element.
        /// The alternative text ("alt" attribute) associated with the channel's image tag when rendered as HTML.
        /// Suggested maximum length of 40 characters.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Required "url" element.
        /// The URL of the image to used in the "src" attribute of the channel's image tag when rendered as HTML.
        /// Suggested maximum length of 500 characters.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Required "link" element.
        /// The URL to which an HTML rendering of the channel image will link. This, as with the channel's title link,
        /// is commonly the parent site's home or news page.
        /// Suggested maximum length of 500 characters.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Extenssions
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}
