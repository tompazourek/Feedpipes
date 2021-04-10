using System.Diagnostics;

namespace Feedpipes.Extensions.CreativeCommons.Entities
{
    /// <summary>
    /// Specifies which Creative Commons license applies.
    /// Corresponds to "cc:license" element.
    /// </summary>
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
    public class CreativeCommonsLicense
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => Value;

        public string Value { get; set; }
    }
}
