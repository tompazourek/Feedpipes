using System;
using System.Collections.Generic;
using Feedpipes.Syndication.Base;
using Feedpipes.Syndication.Extensions.CreativeCommons.Entities;
using Feedpipes.Syndication.Extensions.DublinCore.Entities;
using Feedpipes.Syndication.Extensions.Rss10Syndication.Entities;

namespace Feedpipes.Syndication.FP.Entities
{
    public class FPFeedMetadata : IFeed
    {
        public string Id { get; set; }
        public string Language { get; set; }
        public string RelativeUrlBase { get; set; }

        public FPTextHtml Title { get; set; }
        public FPTextHtml Subtitle { get; set; }
        public FPTextHtml Description { get; set; }
        public FPTextHtml Copyright { get; set; }

        public DateTimeOffset? TimestampModified { get; set; }
        public TimeSpan? Ttl { get; set; }
        public IList<int> SkipHours { get; set; } = new List<int>();
        public IList<DayOfWeek> SkipDays { get; set; } = new List<DayOfWeek>();

        public IList<FPLink> Links { get; set; } = new List<FPLink>();
        public IList<FPImage> Images { get; set; } = new List<FPImage>();
        public IList<FPCategory> Categories { get; set; } = new List<FPCategory>();

        public FPTextInput TextInput { get; set; }
        public FPCloud Cloud { get; set; }
        
        public Rss10SyndicationChannelExtension SyndicationExtension { get; set; }
        public DublinCoreElementExtension DublinCoreExtension { get; set; }
        public CreativeCommonsElementExtension CreativeCommonsExtension { get; set; }
    }
}
