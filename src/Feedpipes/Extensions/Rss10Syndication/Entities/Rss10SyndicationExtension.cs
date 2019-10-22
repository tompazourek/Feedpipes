using System;
using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.Rss10Syndication.Entities
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class Rss10SyndicationExtension : IExtensionEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.UpdateBase)
            .Append(x => x.UpdateFrequency)
            .Append(x => x.UpdatePeriod);

        /// <summary>
        /// Corresponds to "sy:updateBase".
        /// Defines a base date to be used in concert with updatePeriod and updateFrequency to calculate the publishing schedule.
        /// </summary>
        public DateTimeOffset? UpdateBase { get; set; }

        /// <summary>
        /// Corresponds to "sy:updateFrequency".
        /// Used to describe the frequency of updates in relation to the update period. A positive integer indicates how many times
        /// in that period the channel is updated. For example, an updatePeriod of daily, and an updateFrequency of 2 indicates the
        /// channel format is updated twice daily. If omitted a value of 1 is assumed.
        /// </summary>
        public int? UpdateFrequency { get; set; }

        /// <summary>
        /// Corresponds to "sy:updatePeriod".
        /// Describes the period over which the channel format is updated. Acceptable values are: hourly, daily, weekly, monthly,
        /// yearly. If omitted, daily is assumed.
        /// </summary>
        public Rss10SyndicationUpdatePeriodValue UpdatePeriod { get; set; }
    }
}