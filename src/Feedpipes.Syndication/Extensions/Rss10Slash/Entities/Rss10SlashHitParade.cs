using System.Collections.Generic;

namespace Feedpipes.Syndication.Extensions.Rss10Slash.Entities
{
    /// <summary>
    /// Corresponds to "slash:hit_parade" element.
    /// </summary>
    public class Rss10SlashHitParade : IFeedExtensionEntity
    {
        public IList<int> Identifiers { get; set; } = new List<int>();
    }
}