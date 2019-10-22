using System;
using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Extensions;
using Feedpipes.Utils;

namespace Feedpipes.Atom10.Entities
{
    /// <summary>
    /// Corresponds to the "entry" element.
    /// An example of an entry would be a single post on a weblog.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Atom10Entry : IExtensibleEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Id)
            .Append(x => x.Title, x => x.DebuggerDisplay)
            .Append(x => x.Updated);

        /// <summary>
        /// Required "id" element.
        /// Identifies the entry using a universally unique and permanent URI.
        /// Two entries in a feed can have the same value for id if they represent the same entry at different points in time.
        /// </summary>
        /// <example>
        /// http://example.com/blog/1234
        /// </example>
        public string Id { get; set; }

        /// <summary>
        /// Required "title" element.
        /// Contains a human readable title for the entry. This value should not be blank.
        /// </summary>
        /// <example>
        /// Atom-Powered Robots Run Amok
        /// </example>
        public Atom10Text Title { get; set; }

        /// <summary>
        /// Required "updated" element.
        /// Indicates the last time the entry was modified in a significant way. This value need not change after a typo is fixed,
        /// only after a substantial modification. Generally, different entries in a feed will have different updated timestamps.
        /// </summary>
        /// <example>
        /// 2003-12-13T18:30:02-05:00
        /// </example>
        public DateTimeOffset? Updated { get; set; }

        /// <summary>
        /// Corresponds to the "author" elements (Recommended).
        /// Names one author of the entry. An entry may have multiple authors. An entry must contain at least
        /// one author element unless there is an author element in the enclosing feed, or there is an author
        /// element in the enclosed source element.
        /// </summary>
        public IList<Atom10Person> Authors { get; set; } = new List<Atom10Person>();

        /// <summary>
        /// Recommended "content" element.
        /// Contains or links to the complete content of the entry. Content must be provided if there is no
        /// alternate link, and should be provided if there is no summary.
        /// </summary>
        /// <example>
        /// complete story here
        /// </example>
        public Atom10Content Content { get; set; } = new Atom10Content();

        /// <summary>
        /// Corresponds to the "link" elements (Recommended).
        /// Identifies a related Web page. The type of relation is defined by the rel attribute. An entry is limited
        /// to one alternate per type and hreflang. An entry must contain an alternate link if there is no content element.
        /// </summary>
        public IList<Atom10Link> Links { get; set; } = new List<Atom10Link>();

        /// <summary>
        /// Recommended "summary" element.
        /// onveys a short summary, abstract, or excerpt of the entry. Summary should be provided if there either is
        /// no content provided for the entry, or that content is not inline (i.e., contains a src attribute), or if
        /// the content is encoded in base64.
        /// </summary>
        /// <example>
        /// Some text.
        /// </example>
        public Atom10Text Summary { get; set; }

        /// <summary>
        /// Corresponds to the optional "category" elements.
        /// Specifies a category that the entry belongs to. An entry may have multiple category elements.
        /// </summary>
        public IList<Atom10Category> Categories { get; set; } = new List<Atom10Category>();

        /// <summary>
        /// Corresponds to the optional "contributor" elements.
        /// Names one contributor to the entry. An entry may have multiple contributor elements.
        /// </summary>
        public IList<Atom10Person> Contributors { get; set; } = new List<Atom10Person>();

        /// <summary>
        /// Optional "published" element.
        /// Contains the time of the initial creation or first availability of the entry.
        /// </summary>
        /// <example>
        /// 2003-12-13T09:17:51-08:00
        /// </example>
        public DateTimeOffset? Published { get; set; }

        /// <summary>
        /// Optional "rights" element.
        /// Conveys information about rights, e.g. copyrights, held in and over the entry.
        /// </summary>
        /// <example>
        /// © 2005 John Doe
        /// </example>
        public Atom10Text Rights { get; set; }

        /// <summary>
        /// Optional "source" element.
        /// Contains metadata from the source feed if this entry is a copy.
        /// </summary>
        public Atom10Source Source { get; set; }

        /// <summary>
        /// Extenssions
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}