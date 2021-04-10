using System;
using System.Globalization;
using System.Text;

namespace Feedpipes.TimeSpans.MinutesSeconds
{
    public static class MinutesSecondsTimeSpanFormatter
    {
        public static bool TryFormatTimeAsString(TimeSpan? timeToFormat, out string timeFormatted)
        {
            timeFormatted = default;

            if (timeToFormat == null)
                return false;

            var timeFormattedBuilder = new StringBuilder();

            timeFormattedBuilder.Append(Math.Floor(timeToFormat.Value.TotalMinutes).ToString("00", CultureInfo.InvariantCulture));
            timeFormattedBuilder.Append(':');
            timeFormattedBuilder.Append(timeToFormat.Value.Seconds.ToString("00", CultureInfo.InvariantCulture));

            timeFormatted = timeFormattedBuilder.ToString();
            return true;
        }
    }
}
