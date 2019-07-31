namespace Feedpipes.Syndication.Atom10.Entities
{
    /// <summary>
    /// "content" either contains, or links to, the complete content of the entry.
    /// In the most common case, the type attribute is either text, html, xhtml.
    /// Otherwise, if the src attribute is present, it represents the URI of where the content can be found. The type
    /// attribute, if present, is the media type of the content.
    /// Otherwise, if the type attribute ends in +xml or /xml, then an xml document of this type is contained inline.
    /// Otherwise, if the type attribute starts with text, then an escaped document of this type is contained inline.
    /// Otherwise, a base64 encoded document of the indicated media type is contained inline.
    /// </summary>
    public class Atom10Content
    {
        /// <summary>
        /// Corresponds to the "type" attribute.
        /// By default, "text", "html"/"xhtml" otherwise. It can also be a mime type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Corresponds to the "src" attribute.
        /// </summary>
        public string Src { get; set; }

        /// <summary>
        /// Value of the element.
        /// </summary>
        public string Value { get; set; }
    }
}