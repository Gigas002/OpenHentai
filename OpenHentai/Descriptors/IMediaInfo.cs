namespace OpenHentai.Descriptors;

/// <summary>
/// Media info.
/// Can be used for pictures, videos, audio
/// </summary>
public interface IMediaInfo
{
    /// <summary>
    /// Media's external uri
    /// </summary>
    public Uri Source { get; set; }

    /// <summary>
    /// Type of media file
    /// </summary>
    public MediaType Type { get; set; }

    /// <summary>
    /// Is this main media file?
    /// </summary>
    public bool IsMain { get; set; }
}
