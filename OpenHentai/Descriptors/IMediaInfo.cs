namespace OpenHentai.Descriptors;

/// <summary>
/// Media info
/// Can be used for pictures, videos, audio (not yet implemented)
/// </summary>
[Obsolete("Use MediaInfo class")]
public interface IMediaInfo
{
    /// <summary>
    /// Media's external uri
    /// </summary>
    public Uri Source { get; set; }

    // /// <summary>
    // /// Media's path on drive
    // /// </summary>
    // public string MediaPath { get; set; }

    // /// <summary>
    // /// Media's bytes, e.g read from stream
    // /// </summary>
    // public IEnumerable<byte> MediaBytes { get; set; }

    /// <summary>
    /// Type of media file
    /// </summary>
    public MediaType Type { get; set; }

    /// <summary>
    /// Is this main media file?
    /// </summary>
    public bool IsMain { get; set; }
}
