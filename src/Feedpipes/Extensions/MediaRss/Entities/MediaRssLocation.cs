using System;
using System.Collections.Generic;

namespace Feedpipes.Syndication.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Optional element to specify geographical information about various locations captured in the content of a media object.
    /// The format conforms to geoRSS.
    /// </summary>
    public class MediaRssLocation : IExtensibleEntity
    {
        /// <summary>
        /// description description of the place whose location is being specified.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// start time at which the reference to a particular location starts in the media object.
        /// </summary>
        public TimeSpan? Start { get; set; }

        /// <summary>
        /// end time at which the reference to a particular location ends in the media object.
        /// </summary>
        public TimeSpan? End { get; set; }

        /// <summary>
        /// Extensions (for geoRSS and other)
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}