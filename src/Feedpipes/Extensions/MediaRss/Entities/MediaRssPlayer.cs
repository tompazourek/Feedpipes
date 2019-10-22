using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Allows the media object to be accessed through a web browser media player console. This element is required only
    /// if a direct media url attribute is not specified in the "media:content" element.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssPlayer
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Url)
            .Append(x => x.Height)
            .Append(x => x.Width);

        /// <summary>
        /// url is the URL of the player console that plays the media.
        /// It is a required attribute.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// height is the height of the browser window that the URL should be opened in.
        /// It is an optional attribute.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// width is the width of the browser window that the URL should be opened in.
        /// It is an optional attribute.
        /// </summary>
        public int? Width { get; set; }
    }
}