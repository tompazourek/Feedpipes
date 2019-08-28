using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Extensions.CreativeCommons.Entities
{
    /// <summary>
    /// Applies to multiple elements (channel, item)
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class CreativeCommonsExtension : IExtensionEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Licenses);

        public IList<CreativeCommonsLicense> Licenses { get; set; } = new List<CreativeCommonsLicense>();
    }
}