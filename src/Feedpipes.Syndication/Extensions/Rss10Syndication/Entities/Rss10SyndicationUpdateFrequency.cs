namespace Feedpipes.Syndication.Extensions.Rss10Syndication.Entities
{
    /// <summary>
    /// Corresponds to "sy:updateFrequency".
    /// Used to describe the frequency of updates in relation to the update period. A positive integer indicates how many times
    /// in that period the channel is updated. For example, an updatePeriod of daily, and an updateFrequency of 2 indicates the
    /// channel format is updated twice daily. If omitted a value of 1 is assumed.
    /// </summary>
    public class Rss10SyndicationUpdateFrequency : IFeedExtensionEntity
    {
        public int Frequency { get; set; }
    }
}