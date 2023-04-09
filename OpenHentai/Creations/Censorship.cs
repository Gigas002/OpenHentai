namespace OpenHentai.Creations;

/// <summary>
/// Censorship type
/// </summary>
public enum Censorship
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// No censorship, yay!
    /// </summary>
    None = 1,
    
    /// <summary>
    /// Mosaic
    /// </summary>
    Mosaic = 2,
    
    /// <summary>
    /// Tanks (filled blocks)
    /// </summary>
    Tank = 3,
    
    /// <summary>
    /// Everything interesting have been blured
    /// </summary>
    Blur = 4,
    
    /// <summary>
    /// Something different
    /// </summary>
    Other = 5
}
