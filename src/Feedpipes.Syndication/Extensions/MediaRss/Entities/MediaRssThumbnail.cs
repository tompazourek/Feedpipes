using System;
using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Allows particular images to be used as representative images for the media object.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssThumbnail
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Url)
            .Append(x => x.Height)
            .Append(x => x.Width)
            .Append(x => x.Time);

        /// <summary>
        /// url specifies the url of the thumbnail.
        /// It is a required attribute.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// height specifies the height of the thumbnail.
        /// It is an optional attribute.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// width specifies the width of the thumbnail.
        /// It is an optional attribute.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// time specifies the time offset in relation to the media object.
        /// Typically this is used when creating multiple keyframes within a single video.
        /// The format for this attribute should be in the DSM-CC's Normal Play Time (NTP) as used in RTSP [RFC 2326 3.6 Normal
        /// Play Time].
        /// It is an optional attribute.
        /// </summary>
        /// <remarks>
        /// NTP has a second or subsecond resolution. It is specified as H:M:S.h (npt-hhmmss) or S.h (npt-sec),
        /// where H=hours, M=minutes, S=second and h=fractions of a second.
        /// </remarks>
        public TimeSpan? Time { get; set; }
    }
}