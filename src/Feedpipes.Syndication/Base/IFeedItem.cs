using Feedpipes.Syndication.Extensions.CreativeCommons;
using Feedpipes.Syndication.Extensions.DublinCore;
using Feedpipes.Syndication.Extensions.Rss10Content;
using Feedpipes.Syndication.Extensions.Rss10Slash;
using Feedpipes.Syndication.Extensions.WellFormedWeb;

namespace Feedpipes.Syndication.Base
{
    /// <summary>
    /// Corresponds to RSS item or Atom entry.
    /// </summary>
    public interface IFeedItem :
        IExtensibleWithDublinCore,
        IExtensibleWithRss10Content,
        IExtensibleWithWfw,
        IExtensibleWithRss10Slash,
        IExtensibleWithCreativeCommons
    {
    }
}