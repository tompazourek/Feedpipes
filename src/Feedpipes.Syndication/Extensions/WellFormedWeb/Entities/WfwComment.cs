namespace Feedpipes.Syndication.Extensions.WellFormedWeb.Entities
{
    /// <summary>
    /// Corresponds to "wfw:comment".
    /// This element appears in RSS feeds and contains the URI that comment entries are to be POSTed to.
    /// </summary>
    public class WfwComment : IFeedExtensionEntity
    {
        public string Value { get; set; }
    }
}