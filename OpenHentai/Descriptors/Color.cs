namespace OpenHentai.Descriptors;

/// <summary>
/// Controls if creation is colored or not
/// </summary>
// TODO: move to Creations
public enum Color
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Typical black and white manga
    /// </summary>
    BlackWhite = 1,
    
    /// <summary>
    /// Colorized
    /// </summary>
    Colored = 2,
    
    /// <summary>
    /// Has at 40%+ colored
    /// </summary>
    Mixed = 3
}