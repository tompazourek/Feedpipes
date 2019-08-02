using Feedpipes.Syndication.Extensions.RssAtom10.Entities;

namespace Feedpipes.Syndication.Extensions.RssAtom10
{
    public interface IExtensibleWithRssAtom10
    {
        /// <summary>
        /// Optional "atom:*" extended information.
        /// </summary>
        RssAtom10ElementExtension AtomExtension { get; set; }
    }
}