﻿using System.Collections.Generic;

namespace Feedpipes.Rss20
{
    /// <remarks>
    /// More info: https://en.wikipedia.org/wiki/RSS#Variants
    /// </remarks>
    internal static class Rss20Constants
    {
        public static readonly string Version = "2.0";

        public static readonly ISet<string> RecognizedVersions = new HashSet<string>
        {
            "2.0",

            // support previous RSS 2.* branch versions
            "0.91",
            "0.92",
            "0.93",
            "0.94",
        };
    }
}
