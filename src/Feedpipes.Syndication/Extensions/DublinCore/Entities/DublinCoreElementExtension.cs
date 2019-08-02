using System;

namespace Feedpipes.Syndication.Extensions.DublinCore.Entities
{
    /// <summary>
    /// Applies to multiple elements (channel, item, image, textinput)
    /// </summary>
    public class DublinCoreElementExtension
    {
        /// <summary>
        /// Corresponds to "dc:title" element.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Corresponds to "dc:creator" element.
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Corresponds to "dc:subject" element.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Corresponds to "dc:description" element.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Corresponds to "dc:publisher" element.
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Corresponds to "dc:contributor" element.
        /// </summary>
        public string Contributor { get; set; }

        /// <summary>
        /// Corresponds to "dc:date" element.
        /// </summary>
        public DateTimeOffset? Date { get; set; }

        /// <summary>
        /// Corresponds to "dc:modified" element.
        /// </summary>
        public DateTimeOffset? Modified { get; set; }

        /// <summary>
        /// Corresponds to "dc:type" element.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Corresponds to "dc:format" element.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Corresponds to "dc:identifier" element.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Corresponds to "dc:source" element.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Corresponds to "dc:language" element.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Corresponds to "dc:relation" element.
        /// </summary>
        public string Relation { get; set; }

        /// <summary>
        /// Corresponds to "dc:coverage" element.
        /// </summary>
        public string Coverage { get; set; }

        /// <summary>
        /// Corresponds to "dc:rights" element.
        /// </summary>
        public string Rights { get; set; }
    }
}