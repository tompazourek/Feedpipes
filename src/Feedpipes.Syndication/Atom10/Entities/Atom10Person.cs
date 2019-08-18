using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Atom10.Entities
{
    /// <summary>
    /// "author" and "contributor" describe a person, corporation, or similar entity.
    /// It has one required element, name, and two optional elements: uri, email.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Atom10Person
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Name)
            .Append(x => x.Email)
            .Append(x => x.Uri);

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