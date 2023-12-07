using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Constants;
using OpenHentai.Descriptors;

namespace OpenHentai.Creations;

/// <summary>
/// Book, e.g. doujinshi, artbook, etc
/// </summary>
public abstract class Book : Creation
{
    #region Properties

    /// <summary>
    /// Pages count
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// Volumes count
    /// </summary>
    public int Volumes { get; set; }

    /// <summary>
    /// Chapters count
    /// </summary>
    public int Chapters { get; set; }
    
    /// <summary>
    /// Has images, except for covers?
    /// </summary>
    public bool HasImages { get; set; }
    
    /// <summary>
    /// Information about colorization of this book
    /// </summary>
    [Column(TypeName = DataTypes.Jsonb)]
    public HashSet<ColoredInfo> ColoredInfo { get; init; } = [];
    
    #endregion

    #region Constructors

    protected Book() : base() { }

    protected Book(ulong id) : base(id) { }

    protected Book(LanguageSpecificTextInfo title) : base(title) { }

    protected Book(string formattedTitle) : base(formattedTitle) { }

    #endregion
}
