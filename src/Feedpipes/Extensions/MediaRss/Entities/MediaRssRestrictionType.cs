namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// type specifies the type of restriction (country | uri | sharing ) that the media can be syndicated.
    /// </summary>
    public enum MediaRssRestrictionType
    {
        /// <summary>
        /// "country" allows restrictions to be placed based on country code. [ISO 3166]
        /// </summary>
        Country = 0,

        /// <summary>
        /// "uri" allows restrictions based on URI. Examples: urn:apple, http://images.google.com, urn:yahoo, etc.
        /// </summary>
        Uri = 1,

        /// <summary>
        /// "sharing" allows restriction on sharing.
        /// </summary>
        Sharing = 2,
    }
}