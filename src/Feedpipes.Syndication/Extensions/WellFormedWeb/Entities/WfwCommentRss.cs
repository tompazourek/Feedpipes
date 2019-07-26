namespace Feedpipes.Syndication.Extensions.WellFormedWeb.Entities
{
    /// <summary>
    /// Corresponds to "wfw:commentRss".
    /// The second element to appear in the wfw namespace is commentRss. This element also appears in RSS feeds and contains the URI of the RSS feed for comments on that Item.
    /// </summary>
    public class WfwCommentRss : IFeedExtensionEntity
    {
        public string Value { get; set; }
    }
}