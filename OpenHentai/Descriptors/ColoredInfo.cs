using OpenHentai.Creations;

namespace OpenHentai.Descriptors;

/// <summary>
/// Used for manga-like objects that can be bw or colored
/// IBook object should have this property as collection
/// </summary>

// TODO: is it still relevant?
// since search by color must be implemented, it should have it's own
// indexed table:
//
// creations_colors
// creation_id is_colored is_official

public class ColoredInfo
{
    /// <summary>
    /// Creation's color info
    /// </summary>
    public Color Color { get; set; } = Color.Unknown;

    /// <summary>
    /// Is creation colored officialy?
    /// </summary>
    public bool IsOfficial { get; set; }

    /// <summary>
    /// Create new ColoredInfo object
    /// </summary>
    public ColoredInfo() { }

    /// <summary>
    /// Create new ColoredInfo object
    /// </summary>
    /// <param name="color">color</param>
    /// <param name="isOfficial">Is official?</param>
    public ColoredInfo(Color color, bool isOfficial) => (Color, IsOfficial) = (color, isOfficial);
}
