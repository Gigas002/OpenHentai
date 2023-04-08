namespace OpenHentai.Descriptors;

/// <summary>
/// Image info
/// </summary>
public class ImageInfo
{
    #region Properties

    /// <summary>
    /// Image's external uri
    /// </summary>
    public Uri ImageSource { get; set; }

    /// <summary>
    /// Image's path on drive
    /// </summary>
    public string ImagePath { get; set; }

    /// <summary>
    /// Image's bytes, e.g read from stream
    /// </summary>
    public IEnumerable<byte> ImageBytes { get; set; }
    
    #endregion

    #region Constructors

    /// <summary>
    /// Create new image from uri
    /// </summary>
    /// <param name="uri">Source link</param>
    public ImageInfo(Uri uri)
    {
        ImageSource = uri;
    }
    
    #endregion
}
