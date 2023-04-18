namespace OpenHentai.Descriptors;

/// <summary>
/// Media info
/// </summary>
public interface IMediaInfo
{
    /// <summary>
    /// Media's external uri
    /// </summary>
    public Uri MediaSource { get; set; }

    /// <summary>
    /// Media's path on drive
    /// </summary>
    public string MediaPath { get; set; }

    /// <summary>
    /// Media's bytes, e.g read from stream
    /// </summary>
    public IEnumerable<byte> MediaBytes { get; set; }
}
