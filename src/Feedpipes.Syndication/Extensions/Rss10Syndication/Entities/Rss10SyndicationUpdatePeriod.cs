namespace Feedpipes.Syndication.Extensions.Rss10Syndication.Entities
{
    /// <summary>
    /// Corresponds to "sy:updatePeriod".
    /// Describes the period over which the channel format is updated. Acceptable values are: hourly, daily, weekly, monthly,
    /// yearly. If omitted, daily is assumed.
    /// </summary>
    public class Rss10SyndicationUpdatePeriod : IFeedExtensionEntity
    {
        public Rss10SyndicationUpdatePeriodValue Value { get; set; }
    }
}