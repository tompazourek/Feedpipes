namespace Feedpipes.Syndication.Extensions.MediaRss.Entities
{
    /// <summary>
    /// algo indicates the algorithm used to create the hash.
    /// Possible values are "md5" and "sha-1".
    /// Default value is "md5".
    /// </summary>
    public enum MediaRssHashAlgo
    {
        Md5 = 0,
        Sha1 = 1,
    }
}