using System;
using System.Collections.Generic;

namespace Feedpipes.Syndication.Rss20
{
    public class Rss20Item
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Comments { get; set; }
        public string Guid { get; set; }

        public IList<Rss20Category> Categories { get; set; } = new List<Rss20Category>();
        public IList<Rss20Enclosure> Enclosures { get; set; } = new List<Rss20Enclosure>();
        
        public DateTimeOffset? PubDate { get; set; }
        public Rss20Source Source { get; set; }
    }
}