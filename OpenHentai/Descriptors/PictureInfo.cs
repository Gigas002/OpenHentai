namespace OpenHentai.Descriptors;

/// <summary>
/// Picture info
/// </summary>
[Obsolete("Use MediaInfo with MediaType enum")]
public class PictureInfo //: IMediaInfo
{
    // TODO: consider adding IsMain property
    
    #region Properties

    /// <inheritdoc />
    public Uri MediaSource { get; set; }

    /// <inheritdoc />

    public string MediaPath { get; set; }

    /// <inheritdoc />

    public IEnumerable<byte> MediaBytes { get; set; }
    
    #endregion

    #region Constructors

    /// <summary>
    /// Create new picture from uri
    /// </summary>
    /// <param name="uri">Source link</param>
    public PictureInfo(Uri uri) => MediaSource = uri;

    #endregion
}
