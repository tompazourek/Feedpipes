﻿using Feedpipes.Syndication.Base;
using Feedpipes.Syndication.Extensions.DublinCore.Entities;

namespace Feedpipes.Syndication.Rss20.Entities
{
    /// <summary>
    /// Specifies a text input box that can be displayed with the channel.
    /// </summary>
    public class Rss20TextInput : IRssTextInput
    {
        /// <summary>
        /// The label of the Submit button in the text input area.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Explains the text input area.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The name of the text object in the text input area.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The URL of the CGI script that processes text input requests.
        /// </summary>
        public string Link { get; set; }

        public DublinCoreElementExtension DublinCoreExtension { get; set; }
    }
}