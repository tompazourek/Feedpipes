namespace Feedpipes.Syndication.Atom10.Entities
{
    /// <summary>
    /// "author" and "contributor" describe a person, corporation, or similar entity.
    /// It has one required element, name, and two optional elements: uri, email.
    /// </summary>
    public class Atom10Person
    {
        /// <summary>
        /// Required "name" element.
        /// Conveys a human-readable name for the person.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional "uri" element.
        /// Contains a home page for the person.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Optional "email" element.
        /// Contains an email address for the person.
        /// </summary>
        public string Email { get; set; }
    }
}