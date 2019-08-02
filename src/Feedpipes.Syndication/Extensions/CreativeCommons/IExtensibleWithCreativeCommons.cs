using Feedpipes.Syndication.Extensions.CreativeCommons.Entities;

namespace Feedpipes.Syndication.Extensions.CreativeCommons
{
    public interface IExtensibleWithCreativeCommons
    {
        /// <summary>
        /// Optional "cc:*" extended information.
        /// </summary>
        CreativeCommonsElementExtension CreativeCommonsExtension { get; set; }
    }
}