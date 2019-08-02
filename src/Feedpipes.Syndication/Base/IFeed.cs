using Feedpipes.Syndication.Extensions.CreativeCommons;
using Feedpipes.Syndication.Extensions.DublinCore;
using Feedpipes.Syndication.Extensions.Rss10Syndication;

namespace Feedpipes.Syndication.Base
{
    /// <summary>
    /// Corresponds to RSS channel or Atom feed.
    /// </summary>
    public interface IFeed :
        IExtensibleWithRss10Syndication,
        IExtensibleWithDublinCore,
        IExtensibleWithCreativeCommons
    {
    }
}