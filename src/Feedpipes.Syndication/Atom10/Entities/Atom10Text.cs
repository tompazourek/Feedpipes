namespace Feedpipes.Syndication.Atom10.Entities
{
    /// <summary>
    /// "title", "summary", "content", and "rights" contain human-readable text, usually in small quantities. The type
    /// attribute determines how this information is encoded (default="text")
    /// </summary>
    public class Atom10Text
    {
        /// <summary>
        /// Corresponds to the "type" attribute.
        /// By default, "text", "html"/"xhtml" otherwise.
        /// </summary>
        public string Type { get; set; } = "text";

        /// <summary>
        /// Value of the element.
        /// </summary>
        public string Value { get; set; }
    }
}