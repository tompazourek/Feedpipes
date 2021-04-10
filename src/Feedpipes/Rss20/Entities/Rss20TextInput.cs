using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Extensions;
using Feedpipes.Utils;

namespace Feedpipes.Rss20.Entities
{
    /// <summary>
    /// Specifies a text input box that can be displayed with the channel.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss20TextInput : IExtensibleEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Title)
            .Append(x => x.Description)
            .Append(x => x.Name)
            .Append(x => x.Link);

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

        /// <summary>
        /// Extenssions
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}
