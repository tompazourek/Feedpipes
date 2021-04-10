using System;
using System.Collections.Generic;
using System.Globalization;

namespace Feedpipes.TimeSpans.Relaxed
{
    public static class RelaxedTimeSpanParser
    {
        /// <summary>
        /// Relaxed parsing of timespans, assumes that single number is seconds, and two numbers are minutes:seconds.
        /// </summary>
        public static bool TryParseTimeFromStringSecondsFirst(string timeString, out TimeSpan parsedTime)
        {
            parsedTime = default;

            if (string.IsNullOrWhiteSpace(timeString))
                return false;

            timeString = timeString.Trim();

            // A[.B] (seconds.fractions)
            if (TryParseNumber1(timeString, out var seconds))
            {
                parsedTime = TimeSpan.FromSeconds(seconds);
                return true;
            }

            // A:B[.C] (minutes:seconds.fractions)
            if (TryParseNumber2(timeString, out var minutes, out seconds))
            {
                parsedTime = TimeSpan.FromMinutes(minutes) + TimeSpan.FromSeconds(seconds);
                return true;
            }

            // A:B:C[.D] (hours:minutes:seconds.fractions)
            if (TryParseNumber3(timeString, out var hours, out minutes, out seconds))
            {
                parsedTime = TimeSpan.FromHours(hours) + TimeSpan.FromMinutes(minutes) + TimeSpan.FromSeconds(seconds);
                return true;
            }

            // A:B:C:D[.E] (days:hours:minutes:seconds.fractions)
            if (TryParseNumber4(timeString, out var days, out hours, out minutes, out seconds))
            {
                parsedTime = TimeSpan.FromHours(days) + TimeSpan.FromHours(hours) + TimeSpan.FromMinutes(minutes) + TimeSpan.FromSeconds(seconds);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Relaxed parsing of timespans, assumes that single number is minutes, and two numbers are hours:minutes.
        /// </summary>
        public static bool TryParseTimeFromStringMinutesFirst(string timeString, out TimeSpan parsedTime)
        {
            parsedTime = default;

            if (string.IsNullOrWhiteSpace(timeString))
                return false;

            timeString = timeString.Trim();

            // A[.B] (seconds.fractions)
            if (TryParseNumber1(timeString, out var minutes))
            {
                parsedTime = TimeSpan.FromSeconds(minutes);
                return true;
            }

            // A:B[.C] (hours:minutes.fractions)
            if (TryParseNumber2(timeString, out var hours, out minutes))
            {
                parsedTime = TimeSpan.FromMinutes(hours) + TimeSpan.FromSeconds(minutes);
                return true;
            }

            // A:B:C[.D] (hours:minutes:seconds.fractions)
            if (TryParseNumber3(timeString, out hours, out minutes, out var seconds))
            {
                parsedTime = TimeSpan.FromHours(hours) + TimeSpan.FromMinutes(minutes) + TimeSpan.FromSeconds(seconds);
                return true;
            }

            // A:B:C:D[.E] (days:hours:minutes:seconds.fractions)
            if (TryParseNumber4(timeString, out var days, out hours, out minutes, out seconds))
            {
                parsedTime = TimeSpan.FromHours(days) + TimeSpan.FromHours(hours) + TimeSpan.FromMinutes(minutes) + TimeSpan.FromSeconds(seconds);
                return true;
            }

            return false;
        }

        private static bool TryParseNumber1(string input, out double value1)
        {
            value1 = default;

            if (!TryParseNumberParts(input, out var values, out var fractional))
                return false;

            if (values.Count != 1)
                return false;

            value1 = values[0] + fractional;
            return true;
        }

        private static bool TryParseNumber2(string input, out double value1, out double value2)
        {
            value1 = default;
            value2 = default;

            if (!TryParseNumberParts(input, out var values, out var fractional))
                return false;

            if (values.Count != 2)
                return false;

            value1 = values[0];
            value2 = values[1] + fractional;

            return true;
        }

        private static bool TryParseNumber3(string input, out double value1, out double value2, out double value3)
        {
            value1 = default;
            value2 = default;
            value3 = default;

            if (!TryParseNumberParts(input, out var values, out var fractional))
                return false;

            if (values.Count != 3)
                return false;

            value1 = values[0];
            value2 = values[1];
            value3 = values[2] + fractional;

            return true;
        }

        private static bool TryParseNumber4(string input, out double value1, out double value2, out double value3, out double value4)
        {
            value1 = default;
            value2 = default;
            value3 = default;
            value4 = default;

            if (!TryParseNumberParts(input, out var values, out var fractional))
                return false;

            if (values.Count != 4)
                return false;

            value1 = values[0];
            value2 = values[1];
            value3 = values[2];
            value4 = values[3] + fractional;

            return true;
        }

        private static bool TryParseNumberParts(string input, out IList<double> values, out double fractional)
        {
            fractional = default;
            values = default;

            var splitByFractional = input.Split('.');
            if (splitByFractional.Length == 1)
            {
                // only non-fractional part
                if (!TryParseColonSeparated(splitByFractional[0], out values))
                    return false;

                return true;
            }

            if (splitByFractional.Length == 2)
            {
                // non-fractional + fractional part
                if (!TryParseColonSeparated(splitByFractional[0], out values))
                    return false;

                if (!TryParseFractionalNumber(splitByFractional[1], out fractional))
                    return false;

                return true;
            }

            return false;
        }

        private static bool TryParseColonSeparated(string input, out IList<double> values)
        {
            values = default;

            var parts = input.Split(':');
            values = new List<double>();

            foreach (var part in parts)
            {
                if (!TryParseNumber(part, out var parsedDouble))
                    return false;

                values.Add(parsedDouble);
            }

            return true;
        }

        private static bool TryParseNumber(string input, out double value)
            => double.TryParse(input.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out value);

        private static bool TryParseFractionalNumber(string input, out double value)
            => TryParseNumber("0." + input.Trim(), out value);
    }
}
