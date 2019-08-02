using Feedpipes.Syndication.Extensions.DublinCore.Entities;

namespace Feedpipes.Syndication.Extensions.DublinCore
{
    public interface IExtensibleWithDublinCore
    {
        /// <summary>
        /// Optional "dc:*" extended information.
        /// </summary>
        DublinCoreElementExtension DublinCoreExtension { get; set; }
    }
}