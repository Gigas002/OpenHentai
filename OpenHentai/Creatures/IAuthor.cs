using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Descriptors;

namespace OpenHentai.Creatures;

/// <summary>
/// Author
/// </summary>
public interface IAuthor : ICreature
{
    /// <summary>
    /// Author name, e.g. Asanagi
    /// </summary>
    public string AuthorName { get; set; }
    
    /// <summary>
    /// Alternative author names
    /// </summary>
    public IEnumerable<string> AlternativeAuthorNames { get; set; }
    
    /// <summary>
    /// Author's circles
    /// </summary>
    public IEnumerable<ICircle> Circles { get; set; }
    
    /// <summary>
    /// Links to author's social networks, e.g. twitter, pixiv, fanbox, ci-en, etc
    /// </summary>
    public IEnumerable<ExternalLinkInfo> ExternalLinks { get; set; }
    
    /// <summary>
    /// Collection of author works
    /// </summary>
    public IDictionary<ICreation, string> Creations { get; set; }
    // public IEnumerable<ICreation> Creations { get; set; }

    // /// <summary>
    // /// Collection of author's characters
    // /// </summary>
    // public IEnumerable<ICharacter> Characters { get; set; }
}
