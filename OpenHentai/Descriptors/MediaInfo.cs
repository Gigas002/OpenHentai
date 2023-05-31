using OpenHentai.Descriptors;

namespace OpenHentai.Descriptors;

/// <inheritdoc />
public class MediaInfo : IMediaInfo
{
    /// <inheritdoc />
    public Uri Source { get; set; }
    
    /// <inheritdoc />
    public MediaType Type { get; set; }

    /// <inheritdoc />
    public bool IsMain { get; set; }

    public MediaInfo() { }

    public MediaInfo(Uri source, MediaType type, bool isMain = false) =>
        (Source, Type, IsMain) = (source, type, isMain);

    public MediaInfo(string source, MediaType type, bool isMain = false) : this(new Uri(source), type, isMain)
    { }
}
