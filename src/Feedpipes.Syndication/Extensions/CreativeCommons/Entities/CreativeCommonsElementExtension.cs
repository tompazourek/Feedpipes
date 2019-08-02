using System.Collections.Generic;

namespace Feedpipes.Syndication.Extensions.CreativeCommons.Entities
{
    /// <summary>
    /// Applies to multiple elements (channel, item)
    /// </summary>
    public class CreativeCommonsElementExtension
    {
        public IList<CreativeCommonsLicense> Licenses { get; set; } = new List<CreativeCommonsLicense>();
    }
}