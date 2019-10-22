namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// expression determines if the object is a sample or the full version of the object, or even if
    /// it is a continuous stream (sample | full | nonstop). Default value is "full".
    /// </summary>
    public enum MediaRssExpression
    {
        Full = 0,
        Sample = 1,
        Nonstop = 2,
    }
}