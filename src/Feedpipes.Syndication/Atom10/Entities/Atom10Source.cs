using System;

namespace Feedpipes.Syndication.Atom10.Entities
{
    /// <summary>
    /// Corresponds to the "source" element.
    /// Contains metadata from the source feed if this entry is a copy.
    /// </summary>
    public class Atom10Source
    {
        /// <summary>
        /// Corresponds to the "id" element.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Corresponds to the "title" element.
        /// </summary>
        public Atom10Text Title { get; set; }

        /// <summary>
        /// Corresponds to the "updated" element.
        /// </summary>
        public DateTimeOffset? Updated { get; set; }
    }
}