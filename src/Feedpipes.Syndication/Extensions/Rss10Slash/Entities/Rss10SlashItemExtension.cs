using System.Collections.Generic;

namespace Feedpipes.Syndication.Extensions.Rss10Slash.Entities
{
    public class Rss10SlashItemExtension
    {
        /// <summary>
        /// Corresponds to "slash:comments" element.
        /// Represents the number of comments.
        /// </summary>
        public int? Comments { get; set; }

        /// <summary>
        /// Corresponds to "slash:section" element.
        /// </summary>
        public string Section { get; set; }

        /// <summary>
        /// Corresponds to "slash:section" element.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Corresponds to "slash:hit_parade" element.
        /// </summary>
        public IList<int> HitParade { get; set; } = new List<int>();
    }
}