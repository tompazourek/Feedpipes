namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Values of <see cref="MediaRssStatus.State" />
    /// </summary>
    public enum MediaRssStatusState
    {
        /// <summary>
        /// "active" means a media object is active in the system
        /// </summary>
        Active = 0,

        /// <summary>
        /// "blocked" means a media object is blocked by the publisher
        /// </summary>
        Blocked = 1,

        /// <summary>
        /// "deleted" means a media object has been deleted by the publisher
        /// </summary>
        Deleted = 2,
    }
}