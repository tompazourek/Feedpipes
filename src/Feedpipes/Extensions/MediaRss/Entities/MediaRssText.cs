using System;
using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Allows the inclusion of a text transcript, closed captioning or lyrics of the media content.
    /// Many of these elements are permitted to provide a time series of text. In such cases, it is encouraged,
    /// but not required, that the elements be grouped by language and appear in time sequence order based on the start time.
    /// Elements can have overlapping start and end times.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssText : MediaRssTypedText
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal new string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Type)
            .Append(x => x.Value)
            .Append(x => x.Lang)
            .Append(x => x.Start)
            .Append(x => x.End);

        /// <summary>
        /// lang is the primary language encapsulated in the media object. Language codes possible are detailed in RFC 3066.
        /// This attribute is used similar to the xml:lang attribute detailed in the XML 1.0 Specification (Third Edition).
        /// It is an optional attribute.
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// start specifies the start time offset that the text starts being relevant to the media object.
        /// An example of this would be for closed captioning
        /// It uses the NTP time code format (<see cref="MediaRssThumbnail.Time" />).
        /// It is an optional attribute.
        /// </summary>
        public TimeSpan? Start { get; set; }

        /// <summary>
        /// end specifies the end time that the text is relevant.
        /// If this attribute is not provided, and a start time is used, it is expected that the end time is either
        /// the end of the clip or the start of the next <see cref="MediaRssText" /> element.
        /// </summary>
        public TimeSpan? End { get; set; }
    }
}
