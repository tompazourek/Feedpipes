using Feedpipes.Syndication.Extensions.RssAtom10;

namespace Feedpipes.Syndication.Base
{
    /// <summary>
    /// Corresponds to RSS item, but doesn't apply to Atom entry.
    /// </summary>
    public interface IRssItem :
        IExtensibleWithRssAtom10
    {
    }
}