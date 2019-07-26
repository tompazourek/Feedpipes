using System;
using Feedpipes.Syndication.Rfc822Timestamp;
using Xunit;

namespace Feedpipes.Syndication.Tests
{
    public class Rfc822TimestampSerializationTests
    {
        public static object[][] ParseValidInputsData =
        {
            new object[] { "Sun, 19 May 2002 15:21:36 GMT", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "Sun, 19 May 2002 15:21:36 +0000", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
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
        };

        public static object[][] FailParseInvalidInputsData =
        {
            new object[] { "" },
            new object[] { null },
            new object[] { "Sun, 19 May 2002" },
            new object[] { "1 May 2002 1:21:36" },
            new object[] { "10 May 2002 10:1 Z" },
            new object[] { "Sun, 19 May 2002 15:21:36 XYZ" },
        };

        public static object[][] FormatInputsData =
        {
            new object[] { "Sun, 19 May 2002 15:21:36 GMT", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "Sun, 19 May 2002 15:21:36 +0200", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(2)) },
        };

        [Theory]
        [MemberData(nameof(ParseValidInputsData))]
        public void ParseValidInputs(string input, DateTimeOffset expectedOutput)
        {
            // arrange
            var parser = new Rfc822TimestampParser();

            // action
            var tryParseResult = parser.TryParseTimestampFromString(input, out var actualOutput);

            // assert
            Assert.True(tryParseResult);
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Theory]
        [MemberData(nameof(FailParseInvalidInputsData))]
        public void FailParseInvalidInputs(string input)
        {
            // arrange
            var parser = new Rfc822TimestampParser();

            // action
            var tryParseResult = parser.TryParseTimestampFromString(input, out _);

            // assert
            Assert.False(tryParseResult);
        }

        [Fact]
        public void FormatEmpty()
        {
            // arrange
            var formatter = new Rfc822TimestampFormatter();

            // action
            var tryFormatResult = formatter.TryFormatTimestampAsString(null, out _);

            // assert
            Assert.False(tryFormatResult);
        }

        [Theory, MemberData(nameof(FormatInputsData))]
        public void FormatInputs(string expectedOutput, DateTimeOffset input)
        {
            // arrange
            var formatter = new Rfc822TimestampFormatter();

            // action
            var tryFormatResult = formatter.TryFormatTimestampAsString(input, out var actualOutput);

            // assert
            Assert.True(tryFormatResult);
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}