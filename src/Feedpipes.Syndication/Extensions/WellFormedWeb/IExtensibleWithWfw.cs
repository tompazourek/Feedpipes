using Feedpipes.Syndication.Extensions.WellFormedWeb.Entities;

namespace Feedpipes.Syndication.Extensions.WellFormedWeb
{
    public interface IExtensibleWithWfw
    {
        /// <summary>
        /// Optional "wfw:*" extended information.
        /// </summary>
        WfwItemExtension WfwExtension { get; set; }
    }
}