using System;
using System.Globalization;
using System.Text;

namespace Feedpipes.Syndication.Timestamps.Rfc822
{
    public static class Rfc822TimestampFormatter
    {
        public static bool TryFormatTimestampAsString(DateTimeOffset? timestampToFormat, out string formattedTimestamp)
        {
            formattedTimestamp = default;

            if (timestampToFormat == null)
                return false;

            if (timestampToFormat.Value.Offset == TimeSpan.Zero)
            {
                formattedTimestamp = timestampToFormat.Value.ToUniversalTime().ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture) + " GMT";
                return true;
            }

            var sb = new StringBuilder(timestampToFormat.Value.ToString("ddd, dd MMM yyyy HH:mm:ss zzz", CultureInfo.InvariantCulture));

            // the zzz in the format makes the timezone e.g. "-08:00" but we require e.g. "-0800" without the ':'
            sb.Remove(sb.Length - 3, 1);

            formattedTimestamp = sb.ToString();
            return true;
        }
    }
}