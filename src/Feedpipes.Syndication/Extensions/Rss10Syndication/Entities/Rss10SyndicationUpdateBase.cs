using System;

namespace Feedpipes.Syndication.Extensions.Rss10Syndication.Entities
{
    /// <summary>
    /// Corresponds to "sy:updateBase".
    /// Defines a base date to be used in concert with updatePeriod and updateFrequency to calculate the publishing schedule.
    /// </summary>
    public class Rss10SyndicationUpdateBase : IFeedExtensionEntity
    {
        public DateTimeOffset Timestamp { get; set; }
    }
}