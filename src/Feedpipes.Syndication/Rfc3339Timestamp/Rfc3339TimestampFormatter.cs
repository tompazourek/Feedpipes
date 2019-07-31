using System;
using System.Globalization;

namespace Feedpipes.Syndication.Rfc3339Timestamp
{
    public static class Rfc3339TimestampFormatter
    {
        public static bool TryFormatTimestampAsString(DateTimeOffset? timestampToFormat, out string formattedTimestamp)
        {
            formattedTimestamp = default;

            if (timestampToFormat == null)
                return false;

            if (timestampToFormat.Value.Offset == TimeSpan.Zero)
            {
                formattedTimestamp = timestampToFormat.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                return true;
            }

            formattedTimestamp = timestampToFormat.Value.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
            return true;
        }
    }
}