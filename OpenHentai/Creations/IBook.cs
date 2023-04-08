namespace OpenHentai.Creations;

/// <summary>
/// Book, e.g. doujinshi, artbook, etc
/// </summary>
public interface IBook : ICreation
{
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
    /// Is colored?
    /// </summary>
    public bool IsColored { get; set; }
}
