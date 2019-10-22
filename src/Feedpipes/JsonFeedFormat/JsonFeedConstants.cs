using System.Collections.Generic;

namespace Feedpipes.Syndication.JsonFeedFormat
{
    /// <remarks>
    /// More info: https://jsonfeed.org/version/1
    /// </remarks>
    internal static class JsonFeedConstants
    {
        public static readonly string Version = "https://jsonfeed.org/version/1";

        public static readonly ISet<string> RecognizedVersions = new HashSet<string>
        {
            "https://jsonfeed.org/version/1",
            "http://jsonfeed.org/version/1",
        };
    }
}