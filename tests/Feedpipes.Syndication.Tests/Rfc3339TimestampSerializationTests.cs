using System;
using Feedpipes.Syndication.Timestamps.Rfc3339;
using Xunit;

namespace Feedpipes.Syndication.Tests
{
    public class Rfc3339TimestampSerializationTests
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
        };

        public static object[][] FailParseInvalidInputsData =
        {
            new object[] { "" },
            new object[] { null },
            new object[] { "Sun, 19 May 2002" },
            new object[] { "1 May 2002 1:21:36" },
            new object[] { "10 May 2002 10:1 Z" },
            new object[] { "Sun, 19 May 2002 15:21:36 XYZ" },
            new object[] { "Sun, 19 May 2002 15:21:36 GMT" },
            new object[] { "Sun, 19 May 2002 15:21:36 +0000" },
        };

        public static object[][] FormatInputsData =
        {
            new object[] { "2002-05-19T15:21:36Z", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.Zero) },
            new object[] { "2002-05-19T15:21:36Z", new DateTimeOffset(2002, 5, 19, 15, 21, 36, 123, TimeSpan.Zero) },
            new object[] { "2002-05-19T15:21:36+02:00", new DateTimeOffset(2002, 5, 19, 15, 21, 36, TimeSpan.FromHours(2)) },
        };

        [Theory]
        [MemberData(nameof(ParseValidInputsData))]
        public void ParseValidInputs(string input, DateTimeOffset expectedOutput)
        {
            // action
            var tryParseResult = Rfc3339TimestampParser.TryParseTimestampFromString(input, out var actualOutput);

            // assert
            Assert.True(tryParseResult);
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Theory]
        [MemberData(nameof(FailParseInvalidInputsData))]
        public void FailParseInvalidInputs(string input)
        {
            // action
            var tryParseResult = Rfc3339TimestampParser.TryParseTimestampFromString(input, out _);

            // assert
            Assert.False(tryParseResult);
        }

        [Theory]
        [MemberData(nameof(FormatInputsData))]
        public void FormatInputs(string expectedOutput, DateTimeOffset input)
        {
            // action
            var tryFormatResult = Rfc3339TimestampFormatter.TryFormatTimestampAsString(input, out var actualOutput);

            // assert
            Assert.True(tryFormatResult);
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact]
        public void FormatEmpty()
        {
            // action
            var tryFormatResult = Rfc3339TimestampFormatter.TryFormatTimestampAsString(null, out _);

            // assert
            Assert.False(tryFormatResult);
        }
    }
}