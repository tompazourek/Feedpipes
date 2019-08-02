using Feedpipes.Syndication.Extensions.RssAtom10;

namespace Feedpipes.Syndication.Base
{
    /// <summary>
    /// Corresponds to RSS channel, but doesn't apply to Atom feed.
    /// </summary>
    public interface IRssChannel :
        IExtensibleWithRssAtom10
    {
    }
}