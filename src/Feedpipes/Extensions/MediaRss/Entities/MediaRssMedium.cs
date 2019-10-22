namespace Feedpipes.Extensions.MediaRss.Entities
{
    /// <summary>
    /// medium is the type of object (image | audio | video | document | executable). While this attribute can
    /// at times seem redundant if type is supplied, it is included because it simplifies decision making on the
    /// reader side, as well as flushes out any ambiguities between MIME type and object type.
    /// </summary>
    public enum MediaRssMedium
    {
        Image = 0,
        Audio = 1,
        Video = 2,
        Document = 3,
        Executable = 4,
    }
}