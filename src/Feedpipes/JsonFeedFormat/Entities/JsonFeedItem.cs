using System;
using System.Collections.Generic;
using System.Diagnostics;
using Feedpipes.Extensions;
using Feedpipes.Utils;

namespace Feedpipes.JsonFeedFormat.Entities
{
    /// <summary>
    /// items is an array, and is required.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class JsonFeedItem : IExtensibleEntity
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Id)
            .Append(x => x.Title)
            .Append(x => x.Url)
            .Append(x => x.DatePublished);

        /// <summary>
        /// id (required, string) is unique for that item for that feed over time. If an item is ever updated, the id should
        /// be unchanged. New items should never use a previously-used id. If an id is presented as a number or other type,
        /// a JSON Feed reader must coerce it to a string. Ideally, the  id is the full URL of the resource described by the item,
        /// since URLs make great unique identifiers.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// url (optional, string) is the URL of the resource described by the item. It’s the permalink. This may be the same
        /// as the id — but should be present regardless.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// external_url (very optional, string) is the URL of a page elsewhere. This is especially useful for linkblogs.
        /// If url links to where you’re talking about a thing, then external_url links to the thing you’re talking about.
        /// </summary>
        public string ExternalUrl { get; set; }

        /// <summary>
        /// title (optional, string) is plain text. Microblog items in particular may omit titles.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// content_html and content_text are each optional strings — but one or both must be present.
        /// This is the HTML or plain text of the item. Important: the only place HTML is allowed in this
        /// format is in  content_html. A Twitter-like service might use content_text, while a blog might
        /// use content_html. Use whichever makes sense for your resource. (It doesn’t even have to be
        /// the same for each item in a feed.)
        /// </summary>
        public string ContentHtml { get; set; }

        /// <summary>
        /// content_html and content_text are each optional strings — but one or both must be present.
        /// This is the HTML or plain text of the item. Important: the only place HTML is allowed in this
        /// format is in  content_html. A Twitter-like service might use content_text, while a blog might
        /// use content_html. Use whichever makes sense for your resource. (It doesn’t even have to be
        /// the same for each item in a feed.)
        /// </summary>
        public string ContentText { get; set; }

        /// <summary>
        /// summary (optional, string) is a plain text sentence or two describing the item. This might be presented
        /// in a timeline, for instance, where a detail view would display all of content_html or content_text.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// image (optional, string) is the URL of the main image for the item. This image may also appear
        /// in the content_html — if so, it’s a hint to the feed reader that this is the main, featured image.
        /// Feed readers may use the image as a preview (probably resized as a thumbnail and placed in a timeline).
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// banner_image (optional, string) is the URL of an image to use as a banner. Some blogging systems (such as Medium)
        /// display a different banner image chosen to go with each post, but that image wouldn’t otherwise appear in the
        /// content_html.
        /// A feed reader with a detail view may choose to show this banner image at the top of the detail view, possibly
        /// with the title overlaid.
        /// </summary>
        public string BannerImage { get; set; }

        /// <summary>
        /// date_published (optional, string) specifies the date in RFC 3339 format. (Example: 2010-02-07T14:04:00-05:00.)
        /// </summary>
        public DateTimeOffset? DatePublished { get; set; }

        /// <summary>
        /// date_modified (optional, string) specifies the modification date in RFC 3339 format.
        /// </summary>
        public DateTimeOffset? DateModified { get; set; }

        /// <summary>
        /// author (optional, object) has the same structure as the top-level  author. If not specified in an item,
        /// then the top-level author, if present, is the author of the item.
        /// </summary>
        public JsonFeedAuthor Author { get; set; }

        /// <summary>
        /// tags (optional, array of strings) can have any plain text values you want. Tags tend to be just one word,
        /// but they may be anything. Note: they are not the equivalent of Twitter hashtags. Some blogging systems and
        /// other feed formats call these categories.
        /// </summary>
        public IList<string> Tags { get; set; } = new List<string>();

        /// <summary>
        /// attachments (optional, array) lists related resources. Podcasts, for instance, would include an attachment
        /// that’s an audio or video file.
        /// </summary>
        public IList<JsonFeedAttachment> Attachments { get; set; } = new List<JsonFeedAttachment>();

        /// <summary>
        /// Extenssions
        /// </summary>
        public IList<IExtensionEntity> Extensions { get; } = new List<IExtensionEntity>();
    }
}
