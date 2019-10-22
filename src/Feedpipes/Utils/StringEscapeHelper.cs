using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Feedpipes.Syndication.Utils
{
    internal static class StringEscapeHelper
    {
        private static readonly IReadOnlyDictionary<string, string> _escapeMapping = new Dictionary<string, string>
        {
            {"\\\\", "\\"},
            {"\"", "\\\""},
            {"\a", @"\a"},
            {"\b", @"\b"},
            {"\f", @"\f"},
            {"\n", @"\n"},
            {"\r", @"\r"},
            {"\t", @"\t"},
            {"\v", @"\v"},
            {"\0", @"\0"},
        };

        private static readonly Regex _escapeRegex = new Regex(string.Join("|", _escapeMapping.Keys.ToArray()));
        
        public static string Escape(this string s)
            => _escapeRegex.Replace(s, m => _escapeMapping[_escapeMapping.ContainsKey(m.Value) 
                ? m.Value 
                : Regex.Escape(m.Value)]);

    }
}