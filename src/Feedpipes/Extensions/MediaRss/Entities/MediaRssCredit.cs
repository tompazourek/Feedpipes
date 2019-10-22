using System.Diagnostics;
using Feedpipes.Syndication.Utils;

namespace Feedpipes.Syndication.Extensions.MediaRss.Entities
{
    /// <summary>
    /// Notable entity and the contribution to the creation of the media object. Current entities can include people,
    /// companies,
    /// locations, etc. Specific entities can have multiple roles, and several entities can have the same role.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class MediaRssCredit
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => DebuggerDisplayBuilder.Create(this)
            .Append(x => x.Role)
            .Append(x => x.Value)
            .Append(x => x.Scheme);

        /// <summary>
        /// role specifies the role the entity played.
        /// Must be lowercase.
        /// It is an optional attribute.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// scheme is the URI that identifies the role scheme. It is an optional attribute and possible values
        /// for this attribute are ( urn:ebu | urn:yvs ).
        /// The default scheme is "urn:ebu".
        /// The list of roles supported under urn:ebu scheme can be found at European Broadcasting Union Role Codes.
        /// The roles supported under urn:yvs scheme are ( uploader | owner ).
        /// </summary>
        public string Scheme { get; set; } = "urn:ebu";

        public string Value { get; set; }
    }
}