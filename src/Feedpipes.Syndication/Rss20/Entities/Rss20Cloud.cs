namespace Feedpipes.Syndication.Rss20.Entities
{
    /// <summary>
    /// Allows processes to register with a cloud to be notified of updates to the channel, implementing a lightweight
    /// publish-subscribe protocol for RSS feeds.
    /// </summary>
    public class Rss20Cloud
    {
        public string Domain { get; set; }
        public string Port { get; set; }
        public string Path { get; set; }
        public string RegisterProcedure { get; set; }
        public string Protocol { get; set; }
    }
}