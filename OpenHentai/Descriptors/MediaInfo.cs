namespace OpenHentai.Descriptors;

/// <inheritdoc />
public class MediaInfo : IMediaInfo
{
    /// <inheritdoc />
    public Uri Source { get; set; } = null!;
    
    /// <inheritdoc />
    public MediaType Type { get; set; } = MediaType.Unknown;

    /// <inheritdoc />
    public bool IsMain { get; set; }

    /// <summary>
    /// Create new MediaInfo object
    /// </summary>
    public MediaInfo() { }

    /// <summary>
    /// Create new MediaInfo object
    /// </summary>
    /// <param name="source">Media source</param>
    /// <param name="type">Media type</param>
    /// <param name="isMain">Is this main media?</param>
    public MediaInfo(Uri source, MediaType type, bool isMain = false) =>
        (Source, Type, IsMain) = (source, type, isMain);

    /// <summary>
    /// Create new MediaInfo object
    /// </summary>
    /// <param name="source">Media source</param>
    /// <param name="type">Media type</param>
    /// <param name="isMain">Is this main media?</param>
    public MediaInfo(string source, MediaType type, bool isMain = false) : this(new Uri(source), type, isMain)
    { }
}
