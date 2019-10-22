using System.Diagnostics;
using Feedpipes.Utils;

namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Optional tag to include pricing information about a media object. If this tag is not present,
    /// the media object is supposed to be free. One media object can have multiple instances of this
    /// tag for including different pricing structures. The presence of this tag would mean that media
    /// object is not free.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssPrice
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Type)
            .Append(x => x.Info)
            .Append(x => x.Price)
            .Append(x => x.Currency);

        /// <summary>
        /// If nothing is specified, then the media is free.
        /// </summary>
        public MediaRssPriceType? Type { get; set; }

        /// <summary>
        /// info if the type is "package" or "subscription", then info is a URL pointing to package or subscription information.
        /// This is an optional attribute.
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// price is the price of the media object.
        /// This is an optional attribute.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// currency -- use [ISO 4217] for currency codes.
        /// This is an optional attribute.
        /// </summary>
        public string Currency { get; set; }
    }
}