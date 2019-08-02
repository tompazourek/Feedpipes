using System;
using System.Globalization;
using System.Text;

namespace Feedpipes.Syndication.Rfc822Timestamp
{
    public static class Rfc822TimestampParser
    {
        public static bool TryParseTimestampFromString(string timestampString, out DateTimeOffset parsedTimestamp)
        {
            parsedTimestamp = default;

            if (string.IsNullOrWhiteSpace(timestampString))
                return false;

            var timestampStringBuilder = new StringBuilder(timestampString.Trim());
            if (timestampStringBuilder.Length < 18)
                return false;

            if (timestampStringBuilder[3] == ',')
            {
                // there is a leading (e.g.) "Tue, ", strip it off
                timestampStringBuilder.Remove(0, 4);

                // there's supposed to be a space here but some implementations dont have one
                RemoveExtraWhiteSpaceAtStart(timestampStringBuilder);
            }

            ReplaceMultipleWhiteSpaceWithSingleWhiteSpace(timestampStringBuilder);
            if (char.IsDigit(timestampStringBuilder[1]))
            {
                // two-digit day, we are good
            }
            else
            {
                timestampStringBuilder.Insert(0, '0');
            }

            if (timestampStringBuilder.Length < 19)
                return false;

            var thereAreSeconds = timestampStringBuilder[17] == ':';
            var timeZoneStartIndex = thereAreSeconds ? 21 : 18;

            var timeZoneSuffix = timestampStringBuilder.ToString().Substring(timeZoneStartIndex);
            timestampStringBuilder.Remove(timeZoneStartIndex, timestampStringBuilder.Length - timeZoneStartIndex);
            timestampStringBuilder.Append(NormalizeTimeZone(timeZoneSuffix, out var isUtc));
            var wellFormattedString = timestampStringBuilder.ToString();

            var parseFormat = thereAreSeconds ? "dd MMM yyyy HH:mm:ss zzz" : "dd MMM yyyy HH:mm zzz";

            var dateTimeStyles = isUtc ? DateTimeStyles.AssumeUniversal : DateTimeStyles.None;
            var dateTimeFormat = CultureInfo.InvariantCulture.DateTimeFormat;
            if (!DateTimeOffset.TryParseExact(wellFormattedString, parseFormat, dateTimeFormat, dateTimeStyles, out parsedTimestamp))
                return false;

            return true;
        }

        private static string NormalizeTimeZone(string rfc822TimeZone, out bool isUtc)
        {
            isUtc = false;

            // return a string in "-08:00" format
            if (rfc822TimeZone[0] == '+' || rfc822TimeZone[0] == '-')
            {
                // the time zone is supposed to be 4 digits but some feeds omit the initial 0
                var result = new StringBuilder(rfc822TimeZone);
                if (result.Length == 4)
                {
                    // the timezone is +/-HMM. Convert to +/-HHMM
                    result.Insert(1, '0');
                }

                result.Insert(3, ':');
                return result.ToString();
            }

            switch (rfc822TimeZone)
            {
                case "UT":
                case "Z":
                    isUtc = true;
                    return "-00:00";
                case "GMT":
                    return "-00:00";
                case "A":
                    return "-01:00";
                case "B":
                    return "-02:00";
                case "C":
                    return "-03:00";
                case "D":
                case "EDT":
                    return "-04:00";
                case "E":
                case "EST":
                case "CDT":
                    return "-05:00";
                case "F":
                case "CST":
                case "MDT":
                    return "-06:00";
                case "G":
                case "MST":
                case "PDT":
                    return "-07:00";
                case "H":
                case "PST":
                    return "-08:00";
                case "I":
                    return "-09:00";
                case "K":
                    return "-10:00";
                case "L":
                    return "-11:00";
                case "M":
                    return "-12:00";
                case "N":
                    return "+01:00";
                case "O":
                    return "+02:00";
                case "P":
                    return "+03:00";
                case "Q":
                    return "+04:00";
                case "R":
                    return "+05:00";
                case "S":
                    return "+06:00";
                case "T":
                    return "+07:00";
                case "U":
                    return "+08:00";
                case "V":
                    return "+09:00";
                case "W":
                    return "+10:00";
                case "X":
                    return "+11:00";
                case "Y":
                    return "+12:00";
                default:
                    return "";
            }
        }

        private static void RemoveExtraWhiteSpaceAtStart(StringBuilder stringBuilder)
        {
            var i = 0;
            while (i < stringBuilder.Length)
            {
                if (!char.IsWhiteSpace(stringBuilder[i]))
                {
                    break;
                }

                ++i;
            }

            if (i > 0)
            {
                stringBuilder.Remove(0, i);
            }
        }

        private static void ReplaceMultipleWhiteSpaceWithSingleWhiteSpace(StringBuilder builder)
        {
            var index = 0;
            var whiteSpaceStart = -1;
            while (index < builder.Length)
            {
                if (char.IsWhiteSpace(builder[index]))
                {
                    if (whiteSpaceStart < 0)
                    {
                        whiteSpaceStart = index;

                        // normalize all white spaces to be ' ' so that the date time parsing works
                        builder[index] = ' ';
                    }
                }
                else if (whiteSpaceStart >= 0)
                {
                    if (index > whiteSpaceStart + 1)
                    {
                        // there are at least 2 spaces... replace by 1
                        builder.Remove(whiteSpaceStart, index - whiteSpaceStart - 1);
                        index = whiteSpaceStart + 1;
                    }

                    whiteSpaceStart = -1;
                }

                ++index;
            }
        }
    }
}