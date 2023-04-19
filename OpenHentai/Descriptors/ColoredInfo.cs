namespace OpenHentai.Descriptors;

/// <summary>
/// Used for manga-like objects that can be bw or colored
/// IBook object should have this property as collection
/// </summary>

// since search by color must be implemented, it should have it's own
// indexed table:
//
// creations_colors
// creation_id is_colored is_official

// TODO: consider writing enum, containing bw, rgb, rgba values
public class ColoredInfo
{
    /// <summary>
    /// Is creation colored?
    /// </summary>
    public bool IsColored { get; set; }

    /// <summary>
    /// Is cration colored officialy?
    /// </summary>
    public bool IsOfficial { get; set; }
}
