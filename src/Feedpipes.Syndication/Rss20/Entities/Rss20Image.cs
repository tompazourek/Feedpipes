using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Syndication.Extensions;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Rss20.Entities
{
    /// <summary>
    /// Specifies a GIF, JPEG or PNG image that can be displayed with the channel.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss20Image : IExtensibleEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Url)
            .Append(x => x.Title)
            .Append(x => x.Link)
            .Append(x => x.Description)
            .Append(x => x.Width)
            .Append(x => x.Height);

        /// <summary>
        /// URL of a GIF, JPEG or PNG image that represents the channel.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Describes the image, it's used in the ALT attribute of the HTML "img" tag when the channel is rendered in HTML.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// URL of the site, when the channel is rendered, the image is a link to the site.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Contains text that is included in the TITLE attribute of the link formed around the image in the HTML rendering.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Width of the image in pixels.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// Height of the image in pixels.
        /// </summary>
        public int? Height { get; set; }
        
        /// <summary>
        /// Extenssions
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}