using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.JsonFeedFormat.Entities
{
    /// <summary>
    /// An individual item may have one or more attachments.
    /// Podcasts, for instance, would include an attachment that’s an audio or video file.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class JsonFeedAttachment
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Url)
            .Append(x => x.MimeType)
            .Append(x => x.Title)
            .Append(x => x.SizeInBytes)
            .Append(x => x.DurationInSeconds);

        /// <summary>
        /// url (required, string) specifies the location of the attachment.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// mime_type (required, string) specifies the type of the attachment, such as “audio/mpeg.”
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// title (optional, string) is a name for the attachment. Important: if there are multiple attachments,
        /// and two or more have the exact same title (when title is present), then they are considered as alternate
        /// representations of the same thing. In this way a podcaster, for instance, might provide an audio recording
        /// in different formats.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// size_in_bytes (optional, number) specifies how large the file is.
        /// </summary>
        public int? SizeInBytes { get; set; }

        /// <summary>
        /// duration_in_seconds (optional, number) specifies how long it takes to listen to or watch, when played at normal speed.
        /// </summary>
        public int? DurationInSeconds { get; set; }
    }
}