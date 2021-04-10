using System;
using System.Globalization;

namespace Feedpipes.Timestamps.Rfc3339
{
    public static class Rfc3339TimestampParser
    {
        private static readonly string[] SupportedFormatsWithOffset =
        {
            "yyyy-MM-ddTHH:mm:ss.fffffffzzz",
            "yyyy-MM-ddTHH:mm:ss.ffffffzzz",
            "yyyy-MM-ddTHH:mm:ss.fffffzzz",
            "yyyy-MM-ddTHH:mm:ss.ffffzzz",
            "yyyy-MM-ddTHH:mm:ss.fffzzz",
            "yyyy-MM-ddTHH:mm:ss.ffzzz",
            "yyyy-MM-ddTHH:mm:ss.fzzz",
            "yyyy-MM-ddTHH:mm:sszzz",
            "yyyy-MM-ddTHH:mmzzz",
        };

        private static readonly string[] SupportedFormatsWithoutOffset =
        {
            "yyyy-MM-ddTHH:mm:ss.fffffffZ",
            "yyyy-MM-ddTHH:mm:ss.ffffffZ",
            "yyyy-MM-ddTHH:mm:ss.fffffZ",
            "yyyy-MM-ddTHH:mm:ss.ffffZ",
            "yyyy-MM-ddTHH:mm:ss.fffZ",
            "yyyy-MM-ddTHH:mm:ss.ffZ",
            "yyyy-MM-ddTHH:mm:ss.fZ",
            "yyyy-MM-ddTHH:mm:ssZ",
            "yyyy-MM-ddTHH:mmZ",
        };

        public static bool TryParseTimestampFromString(string timestampString, out DateTimeOffset parsedTimestamp)
        {
            parsedTimestamp = default;

            if (string.IsNullOrWhiteSpace(timestampString))
                return false;

            timestampString = timestampString.Trim().ToUpperInvariant();

            if (DateTimeOffset.TryParseExact(timestampString, SupportedFormatsWithoutOffset, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out parsedTimestamp))
                return true;

            if (DateTimeOffset.TryParseExact(timestampString, SupportedFormatsWithOffset, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedTimestamp))
                return true;

            return false;
        }
    }
}
