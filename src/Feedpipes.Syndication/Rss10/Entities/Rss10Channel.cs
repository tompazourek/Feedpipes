using System;
using System.Collections.Generic;
using Feedpipes.Syndication.Extensions.Rss10Content.Entities;
using Feedpipes.Syndication.Extensions.Rss10Syndication.Entities;

namespace Feedpipes.Syndication.Rss10.Entities
{
    public class Rss10Channel
    {
        /// <summary>
        /// Required "rdf:about" attribute.
        /// The {resource} URL of the channel element's rdf:about attribute must be unique with respect to any other
        /// rdf:about attributes in the RSS document and is a URI which identifies the channel. Most commonly, this is
        /// either the URL of the homepage being described or a URL where the RSS file can be found.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Required "title" element.
        /// A descriptive title for the channel.
        /// Suggested maximum length of 40 characters.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Required "link" element.
        /// The URL to which an HTML rendering of the channel title will link, commonly the parent site's home or news page.
        /// Suggested maximum length of 500 characters.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Required "description" element.
        /// A brief description of the channel's content, function, source, etc.
        /// Suggested maximum length of 500 characters.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Optional "image" element.
        /// An image to be associated with an HTML rendering of the channel. This image should be of
        /// a format supported by the majority of Web browsers. While the later 0.91 specification
        /// allowed for a width of 1-144 and height of 1-400, convention (and the 0.9 specification) dictate 88x31.
        /// </summary>
        public Rss10Image Image { get; set; }

        /// <summary>
        /// Optional "textInput" element.
        /// The textinput element affords a method for submitting form data to an arbitrary URL -- usually located at the parent website.
        /// </summary>
        public Rss10TextInput TextInput { get; set; }

        /// <summary>
        /// List of "item" elements.
        /// </summary>
        public IList<Rss10Item> Items { get; set; } = new List<Rss10Item>();

        /// <summary>
        /// Optional "sy:*" extended information.
        /// </summary>
        public Rss10SyndicationChannelExtension SyndicationExtension { get; set; }
        
        /// <summary>
        /// Optional "dc:*" extended information.
        /// </summary>
        public DublinCoreElementExtension DublinCoreExtension { get; set; }
    }
}