using System.Collections.Generic;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Contains information that can be either on the top-level of the extension, inside a <see cref="MediaRssGroup" />, or
    /// inside a <see cref="MediaRssContent" />.
    /// </summary>
    public abstract class MediaRssContainer
    {
        /// <summary>
        /// This allows the permissible audience to be declared. If this element is not included, it assumes that
        /// no restrictions are necessary.
        /// </summary>
        public IList<MediaRssRating> Ratings { get; set; } = new List<MediaRssRating>();

        /// <summary>
        /// The title of the particular media object.
        /// </summary>
        public MediaRssTypedText Title { get; set; }

        /// <summary>
        /// Short description describing the media object typically a sentence in length.
        /// </summary>
        public MediaRssTypedText Description { get; set; }

        /// <summary>
        /// Highly relevant keywords describing the media object with typically a maximum of 10 words.
        /// </summary>
        public IList<string> Keywords { get; set; } = new List<string>();

        /// <summary>
        /// Allows particular images to be used as representative images for the media object.
        /// If multiple thumbnails are included, and time coding is not at play, it is assumed that the images
        /// are in order of importance.
        /// </summary>
        public IList<MediaRssThumbnail> Thumbnails { get; set; } = new List<MediaRssThumbnail>();

        /// <summary>
        /// Allows a taxonomy to be set that gives an indication of the type of media content, and its particular contents.
        /// </summary>
        public IList<MediaRssCategory> Categories { get; set; } = new List<MediaRssCategory>();

        /// <summary>
        /// This is the hash of the binary media file. It can appear multiple times as long as each instance is a different algo.
        /// </summary>
        public IList<MediaRssHash> Hashes { get; set; } = new List<MediaRssHash>();

        /// <summary>
        /// Allows the media object to be accessed through a web browser media player console. This element is required only
        /// if a direct media url attribute is not specified in the "media:content" element.
        /// </summary>
        public MediaRssPlayer Player { get; set; }

        /// <summary>
        /// Notable entity and the contribution to the creation of the media object. Current entities can include people,
        /// companies,
        /// locations, etc. Specific entities can have multiple roles, and several entities can have the same role.
        /// </summary>
        public IList<MediaRssCredit> Credits { get; set; } = new List<MediaRssCredit>();

        /// <summary>
        /// Copyright information for the media object.
        /// </summary>
        public MediaRssCopyright Copyright { get; set; }

        /// <summary>
        /// Allows the inclusion of a text transcript, closed captioning or lyrics of the media content.
        /// Many of these elements are permitted to provide a time series of text. In such cases, it is encouraged,
        /// but not required, that the elements be grouped by language and appear in time sequence order based on the start time.
        /// Elements can have overlapping start and end times.
        /// </summary>
        public IList<MediaRssText> Texts { get; set; } = new List<MediaRssText>();

        /// <summary>
        /// Allows restrictions to be placed on the aggregator rendering the media in the feed. Currently, restrictions are
        /// based on distributor (URI), country codes and sharing of a media object. This element is purely informational and
        /// no obligation can be assumed or implied.
        /// </summary>
        public IList<MediaRssRestriction> Restrictions { get; set; } = new List<MediaRssRestriction>();

        /// <summary>
        /// This element stands for the community related content. This allows inclusion of the user perception about a media
        /// object
        /// in the form of view count, ratings and tags.
        /// </summary>
        public MediaRssCommunity Community { get; set; }

        /// <summary>
        /// Allows inclusion of all the comments a media object has received.
        /// </summary>
        public IList<string> Comments { get; set; } = new List<string>();

        /// <summary>
        /// Sometimes player-specific embed code is needed for a player to play any video.
        /// </summary>
        public IList<MediaRssEmbed> Embeds { get; set; } = new List<MediaRssEmbed>();

        /// <summary>
        /// Allows inclusion of a list of all media responses a media object has received.
        /// </summary>
        public IList<string> Responses { get; set; } = new List<string>();

        /// <summary>
        /// Allows inclusion of all the URLs pointing to a media object.
        /// </summary>
        public IList<string> BackLinks { get; set; } = new List<string>();

        /// <summary>
        /// Specifies the status of a media object -- whether it's still active or it has been blocked/deleted.
        /// </summary>
        public MediaRssStatus Status { get; set; }

        /// <summary>
        /// Optional tag to include pricing information about a media object. If this tag is not present,
        /// the media object is supposed to be free. One media object can have multiple instances of this
        /// tag for including different pricing structures. The presence of this tag would mean that media
        /// object is not free.
        /// </summary>
        public IList<MediaRssPrice> Prices { get; set; } = new List<MediaRssPrice>();

        /// <summary>
        /// Optional link to specify the machine-readable license associated with the content.
        /// </summary>
        public MediaRssLink License { get; set; }

        /// <summary>
        /// Optional element for subtitle/CC link. It contains type and language attributes.
        /// Language is based on RFC 3066. There can be more than one such tag per media element, for example one per language.
        /// Please refer to Timed Text spec - W3C for more information on Timed Text and Real Time Subtitling.
        /// </summary>
        public IList<MediaRssLink> SubTitles { get; set; } = new List<MediaRssLink>();

        /// <summary>
        /// Optional elements for P2P links.
        /// </summary>
        public IList<MediaRssLink> PeerLinks { get; set; } = new List<MediaRssLink>();

        /// <summary>
        /// Optional elements to specify geographical information about various locations captured in the content of a media
        /// object.
        /// The format conforms to geoRSS.
        /// </summary>
        public IList<MediaRssLocation> Locations { get; set; } = new List<MediaRssLocation>();

        /// <summary>
        /// Optional element to specify the rights information of a media object saying whether a media object has
        /// been created by the publisher or they have rights to circulate it.
        /// </summary>
        public MediaRssRightsStatus? Rights { get; set; }

        /// <summary>
        /// Optional elements to specify various scenes within a media object.
        /// </summary>
        public IList<MediaRssScene> Scenes { get; set; } = new List<MediaRssScene>();
    }
}
