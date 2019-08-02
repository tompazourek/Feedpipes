using System;
using Feedpipes.Syndication.RelaxedTimestamp;
using Xunit;

namespace Feedpipes.Syndication.Tests
{
    public class RelaxedTimestampSerializationTests
    {
        public static object[][] ParseValidInputsData =
        {
            new object[] { "2002-05-19T15:21:36Z", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "   2002-05-19T15:21:36Z  ", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "2002-05-19T15:21:36.123Z", new DateTimeOffset(2002, 5, 19, 15, 21, 36, 123, TimeSpan.Zero) },
            new object[] { "2002-05-19T15:21:36+02:00", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(2)) },
            new object[] { "2002-10-02T10:00:00-05:00", new DateTimeOffset(2002, 10, 2, 10, 0, 0, TimeSpan.FromHours(-5)) },
            new object[] { "2002-10-02T10:00:00Z", new DateTimeOffset(2002, 10, 2, 10, 0, 0, TimeSpan.Zero) },
            new object[] { "2002-10-02T15:00:00.05Z", new DateTimeOffset(2002, 10, 2, 15, 0, 0, 50, TimeSpan.Zero) },
            new object[] { "Sun, 19 May 2002 15:21:36 GMT", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "Sun, 19 May 2002 15:21:36 +400", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(4)) },
            new object[] { "Sun, 19 May 2002 15:21:36 -0000", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "Sun, 01 May 2002 15:21:36 GMT", new DateTimeOffset(2002, 5, 1, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "Sun, 1 May 2002 15:21:36 GMT", new DateTimeOffset(2002, 5, 1, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "     Sun,   19   May   2002    15:21:36    GMT    ", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "Sun, 19 May 2002 15:21:36 UT", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "Sun, 19 May 2002 15:21:36 Z", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "Sun, 19 May 2002 15:21:36 GMT", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "Sun, 19 May 2002 15:21:36 A", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-01)) },
            new object[] { "Sun, 19 May 2002 15:21:36 B", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-02)) },
            new object[] { "Sun, 19 May 2002 15:21:36 C", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-03)) },
            new object[] { "Sun, 19 May 2002 15:21:36 D", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-04)) },
            new object[] { "Sun, 19 May 2002 15:21:36 EDT", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-04)) },
            new object[] { "Sun, 19 May 2002 15:21:36 E", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-05)) },
            new object[] { "Sun, 19 May 2002 15:21:36 EST", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-05)) },
            new object[] { "Sun, 19 May 2002 15:21:36 CDT", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-05)) },
            new object[] { "Sun, 19 May 2002 15:21:36 F", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-06)) },
            new object[] { "Sun, 19 May 2002 15:21:36 CST", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-06)) },
            new object[] { "Sun, 19 May 2002 15:21:36 MDT", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-06)) },
            new object[] { "Sun, 19 May 2002 15:21:36 G", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-07)) },
            new object[] { "Sun, 19 May 2002 15:21:36 MST", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-07)) },
            new object[] { "Sun, 19 May 2002 15:21:36 PDT", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-07)) },
            new object[] { "Sun, 19 May 2002 15:21:36 H", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-08)) },
            new object[] { "Sun, 19 May 2002 15:21:36 PST", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-08)) },
            new object[] { "Sun, 19 May 2002 15:21:36 I", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-09)) },
            new object[] { "Sun, 19 May 2002 15:21:36 K", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-10)) },
            new object[] { "Sun, 19 May 2002 15:21:36 L", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-11)) },
            new object[] { "Sun, 19 May 2002 15:21:36 M", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(-12)) },
            new object[] { "Sun, 19 May 2002 15:21:36 N", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+01)) },
            new object[] { "Sun, 19 May 2002 15:21:36 O", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+02)) },
            new object[] { "Sun, 19 May 2002 15:21:36 P", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+03)) },
            new object[] { "Sun, 19 May 2002 15:21:36 Q", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+04)) },
            new object[] { "Sun, 19 May 2002 15:21:36 R", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+05)) },
            new object[] { "Sun, 19 May 2002 15:21:36 S", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+06)) },
            new object[] { "Sun, 19 May 2002 15:21:36 T", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+07)) },
            new object[] { "Sun, 19 May 2002 15:21:36 U", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+08)) },
            new object[] { "Sun, 19 May 2002 15:21:36 V", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+09)) },
            new object[] { "Sun, 19 May 2002 15:21:36 W", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+10)) },
            new object[] { "Sun, 19 May 2002 15:21:36 X", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+11)) },
            new object[] { "Sun, 19 May 2002 15:21:36 Y", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(+12)) },
            new object[] { "Sun, 19 May 2002 15:21:36 +0200", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(2)) },
            new object[] { "2000-01-01T12:00+00:00", new DateTimeOffset(2000, 1, 1, 12, 0, 0, TimeSpan.Zero), },
            new object[] { "2019-08-01T20:36:29Z", new DateTimeOffset(2019, 8, 1, 20, 36, 29, TimeSpan.Zero), },
            new object[] { "2019-08-01T21:17:36+01:00", new DateTimeOffset(2019, 8, 1, 21, 17, 36, TimeSpan.FromHours(1)), },
            new object[] { "2019-08-01", new DateTimeOffset(2019, 8, 1, 0, 0, 0, TimeSpan.Zero), },
            new object[] { "2019-08-01T17:04:00+00:00", new DateTimeOffset(2019, 8, 1, 17, 4, 0, TimeSpan.Zero), },
            new object[] { "2019-08-01T10:40:00-07:00", new DateTimeOffset(2019, 8, 1, 10, 40, 0, TimeSpan.FromHours(-7)), },
            new object[] { "Thu, 01 Aug 2019 20:20:27 +0000", new DateTimeOffset(2019, 8, 1, 20, 20, 27, TimeSpan.Zero), },
            new object[] { "Sun, 19 May 2002", new DateTimeOffset(2002, 5, 19, 0, 0, 0, TimeSpan.Zero), },
            new object[] { "1 May 2002 1:21:36", new DateTimeOffset(2002, 5, 1, 1, 21, 36, TimeSpan.Zero), },
            new object[] { "10 May 2002 10:1 Z", new DateTimeOffset(2002, 5, 10, 10, 1, 0, TimeSpan.Zero), },
            new object[] { "Sun, 19 May 2002 15:21:36 +0000", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero), },
        };

        public static object[][] FailParseInvalidInputsData =
        {
            new object[] { "" },
            new object[] { null },
            new object[] { "Sun, 19 May 2002 15:21:36 XYZ" },
        };

        [Theory]
        [MemberData(nameof(ParseValidInputsData))]
        public void ParseValidInputs(string input, DateTimeOffset expectedOutput)
        {
            // action
            var tryParseResult = RelaxedTimestampParser.TryParseTimestampFromString(input, out var actualOutput);

            // assert
            Assert.True(tryParseResult);
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Theory]
        [MemberData(nameof(FailParseInvalidInputsData))]
        public void FailParseInvalidInputs(string input)
        {
            // action
            var tryParseResult = RelaxedTimestampParser.TryParseTimestampFromString(input, out _);

            // assert
            Assert.False(tryParseResult);
        }
    }
}