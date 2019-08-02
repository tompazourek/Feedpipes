namespace Feedpipes.Syndication.Atom10.Entities
{
    /// <summary>
    /// "link" is patterned after html's link element. It has one required attribute, href, and five optional attributes:
    /// rel, type, hreflang, title, and length.
    /// </summary>
    public class Atom10Link
    {
        /// <summary>
        /// Required "href" attribute.
        /// href is the URI of the referenced resource (typically a Web page).
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Optional "rel" attribute.
        /// rel contains a single link relationship type. It can be a full URI (see extensibility),
        /// or one of the following predefined values (default=alternate):
        /// - alternate: an alternate representation of the entry or feed, for example a permalink to the html version of the
        /// entry, or the front page of the weblog.
        /// - enclosure: a related resource which is potentially large in size and might require special handling, for example an
        /// audio or video recording.
        /// - related: an document related to the entry or feed.
        /// - self: the feed itself.
        /// - via: the source of the information provided in the entry.
        /// </summary>
        public string Rel { get; set; } = "alternate";

        /// <summary>
        /// Optional "type" attribute.
        /// type indicates the media type of the resource.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Optional "hreflang" attribute.
        /// hreflang indicates the language of the referenced resource.
        /// </summary>
        public string Hreflang { get; set; }

        /// <summary>
        /// Optional "title" attribute.
        /// title human readable information about the link, typically for display purposes.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Optional "length" attribute.
        /// length the length of the resource, in bytes.
        /// </summary>
        public int? Length { get; set; }
    }
}