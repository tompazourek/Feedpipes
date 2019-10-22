using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Syndication.Extensions;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Rss10.Entities
{
    /// <summary>
    /// The textinput element affords a method for submitting form data to an arbitrary URL -- usually located
    /// at the parent website. The form processor at the receiving end only is assumed to handle the HTTP GET method.
    /// The field is typically used as a search box or subscription form -- among others. While this is of some use
    /// when RSS documents are rendered as channels (see MNN) and accompanied by human readable title and description,
    /// the ambiguity in automatic determination of meaning of this overloaded element renders it otherwise not
    /// particularly useful. RSS 1.0 therefore suggests either deprecation or augmentation with some form of resource
    /// discovery of this element in future versions while maintaining it for backward compatiblity with RSS 0.9.
    /// {textinput_uri} must be unique with respect to any other rdf:about attributes in the RSS document and is
    /// a URI which identifies the textinput. {textinput_uri} should be identical to the value of the "link"
    /// sub-element of the "textinput" element, if possible.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss10TextInput : IExtensibleEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Title)
            .Append(x => x.Description)
            .Append(x => x.Name)
            .Append(x => x.Link);

        /// <summary>
        /// Required "rdf:about" attribute.
        /// It's usually the same as the link.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Required "title" element.
        /// A descriptive title for the textinput field. For example: "Subscribe" or "Search!".
        /// Suggested maximum length of 40 characters.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Required "description" element.
        /// A brief description of the textinput field's purpose. For example: "Subscribe to our newsletter for..." or "Search our
        /// site's archive of..."
        /// Suggested maximum length of 100 characters.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Required "name" element.
        /// The text input field's (variable) name.
        /// Suggested maximum length of 500 characters.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Required "link" element.
        /// The URL to which a textinput submission will be directed (using GET).
        /// Suggested maximum length of 500 characters.
        /// </summary>
        public string Link { get; set; }
        
        /// <summary>
        /// Extenssions
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}