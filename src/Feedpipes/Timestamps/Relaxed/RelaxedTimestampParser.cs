using System;
using System.Globalization;
using Feedpipes.Timestamps.Rfc3339;
using Feedpipes.Timestamps.Rfc822;

namespace Feedpipes.Timestamps.Relaxed
{
    public static class RelaxedTimestampParser
    {
        private static readonly string[] OtherFormatsWithOffset =
        {
            "yyyy-MM-ddzzz",
        };

        private static readonly string[] OtherFormatsWithoutOffset =
        {
            "yyyy-MM-ddZ",
            "yyyy-MM-dd",
        };

        public static bool TryParseTimestampFromString(string timestampString, out DateTimeOffset parsedTimestamp)
        {
            parsedTimestamp = default;

            if (string.IsNullOrWhiteSpace(timestampString))
                return false;

            timestampString = timestampString.Trim();

            if (Rfc3339TimestampParser.TryParseTimestampFromString(timestampString, out parsedTimestamp))
                return true;

            if (Rfc822TimestampParser.TryParseTimestampFromString(timestampString, out parsedTimestamp))
                return true;

            // try other formats
            timestampString = timestampString.ToUpperInvariant();

            if (DateTimeOffset.TryParseExact(timestampString, OtherFormatsWithoutOffset, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out parsedTimestamp))
                return true;

            if (DateTimeOffset.TryParseExact(timestampString, OtherFormatsWithOffset, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedTimestamp))
                return true;

            // try default parsing
            if (DateTimeOffset.TryParse(timestampString, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out parsedTimestamp))
                return true;

            return false;
        }
    }
}
