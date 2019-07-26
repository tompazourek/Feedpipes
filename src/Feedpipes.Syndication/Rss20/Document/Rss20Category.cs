namespace Feedpipes.Syndication.Rss20.Document
{
    /// <summary>
    /// Feed or item category.
    /// </summary>
    public class Rss20Category
    {
        /// <summary>
        /// The value of the element is a forward-slash-separated string that identifies
        /// a hierarchic location in the indicated taxonomy. Processors may establish conventions
        /// for the interpretation of categories.
        /// </summary>
        /// <example>
        /// Grateful Dead
        /// </example>
        public string Name { get; set; }

        /// <summary>
        /// Domain, a string that identifies a categorization taxonomy.
        /// </summary>
        /// <example>
        /// http://www.fool.com/cusips
        /// </example>
        public string Domain { get; set; }
    }
}