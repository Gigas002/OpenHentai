namespace OpenHentai.Creations;

/// <summary>
/// Controls if creation is colored or not
/// </summary>
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
    /// Has at least 40%+ colored
    /// </summary>
    Mixed = 3
}