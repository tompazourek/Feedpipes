using System;
using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// "media:content" is a sub-element of either "item" or "media:group". Media objects that are not the same
    /// content should not be included in the same "media:group" element. The sequence of these items implies
    /// the order of presentation. While many of the attributes appear to be audio/video specific, this element
    /// can be used to publish any type of media. It contains 14 attributes, most of which are optional.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssContent : MediaRssContainer, IExtensibleEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Url)
            .Append(x => x.Type)
            .Append(x => x.Medium);

        /// <summary>
        /// url should specify the direct URL to the media object.
        /// If not included, a "media:player" element must be specified.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// fileSize is the number of bytes of the media object.
        /// It is an optional attribute.
        /// </summary>
        public int? FileSize { get; set; }

        /// <summary>
        /// type is the standard MIME type of the object.
        /// It is an optional attribute.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// medium is the type of object (image | audio | video | document | executable).
        /// While this attribute can at times seem redundant if type is supplied, it is included
        /// because it simplifies decision making on the reader side, as well as flushes out any
        /// ambiguities between MIME type and object type.
        /// It is an optional attribute.
        /// </summary>
        public MediaRssMedium? Medium { get; set; }

        /// <summary>
        /// isDefault determines if this is the default object that should be used for the "media:group".
        /// There should only be one default object per "media:group".
        /// It is an optional attribute.
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// expression determines if the object is a sample or the full version of the object, or even if
        /// it is a continuous stream (sample | full | nonstop).
        /// Default value is "full".
        /// It is an optional attribute.
        /// </summary>
        public MediaRssExpression Expression { get; set; }

        /// <summary>
        /// bitrate is the kilobits per second rate of media.
        /// It is an optional attribute.
        /// </summary>
        public double? BitRate { get; set; }

        /// <summary>
        /// framerate is the number of frames per second for the media object.
        /// It is an optional attribute.
        /// </summary>
        public double? FrameRate { get; set; }

        /// <summary>
        /// samplingrate is the number of samples per second taken to create the media object.
        /// It is expressed in thousands of samples per second (kHz).
        /// It is an optional attribute.
        /// </summary>
        public double? SamplingRate { get; set; }

        /// <summary>
        /// channels is number of audio channels in the media object.
        /// It is an optional attribute.
        /// </summary>
        public int? Channels { get; set; }

        /// <summary>
        /// duration is the number of seconds the media object plays.
        /// It is an optional attribute.
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// height is the height of the media object.
        /// It is an optional attribute.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// width is the width of the media object.
        /// It is an optional attribute.
        /// </summary>
        public int? Width { get; set; }

        /// <summary>
        /// lang is the primary language encapsulated in the media object.
        /// Language codes possible are detailed in RFC 3066.
        /// This attribute is used similar to the xml:lang attribute detailed in the XML 1.0 Specification (Third Edition).
        /// It is an optional attribute.
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Extensions (for Dublin Core and other)
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}
