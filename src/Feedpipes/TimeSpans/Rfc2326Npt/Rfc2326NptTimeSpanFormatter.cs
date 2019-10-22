using System;
using System.Globalization;
using System.Text;

namespace Feedpipes.TimeSpans.Rfc2326Npt
{
    public static class Rfc2326NptTimeSpanFormatter
    {
        public static bool TryFormatTimeAsString(TimeSpan? timeToFormat, out string timeFormatted)
        {
            timeFormatted = default;

            if (timeToFormat == null)
                return false;

            var timeFormattedBuilder = new StringBuilder();

            timeFormattedBuilder.Append(Math.Floor(timeToFormat.Value.TotalHours).ToString("00", CultureInfo.InvariantCulture));
            timeFormattedBuilder.Append(':');
            timeFormattedBuilder.Append(timeToFormat.Value.Minutes.ToString("00", CultureInfo.InvariantCulture));
            timeFormattedBuilder.Append(':');
            timeFormattedBuilder.Append(timeToFormat.Value.Seconds.ToString("00", CultureInfo.InvariantCulture));
            timeFormattedBuilder.Append('.');
            timeFormattedBuilder.Append(timeToFormat.Value.Milliseconds.ToString("000", CultureInfo.InvariantCulture));

            timeFormatted = timeFormattedBuilder.ToString();
            return true;
        }
    }
}