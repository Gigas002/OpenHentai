using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Descriptors;
using OpenHentai.Roles;

namespace OpenHentai.Creatures;

/// <summary>
/// Author
/// </summary>
public interface IAuthor : ICreature
{
    /// <summary>
    /// Main name must be romanization of native name (e.g. Hepburn romanization for ja-JP)
    /// Alternative names can be any
    /// </summary>
    // public IEnumerable<LanguageSpecificTextInfo> AuthorNames { get; set; }
    public IEnumerable<LanguageSpecificTextInfo> GetAuthorNames();
    
    /// <summary>
    /// Author's circles
    /// </summary>
    // public IEnumerable<ICircle> Circles { get; set; }
    public IEnumerable<ICircle> GetCircles();
    
    /// <summary>
    /// Links to author's social networks, e.g. twitter, pixiv, fanbox, ci-en, etc
    /// </summary>
    public IEnumerable<ExternalLinkInfo> ExternalLinks { get; set; }
    
    /// <summary>
    /// Collection of author works
    /// </summary>
    // public IDictionary<ICreation, AuthorRole> Creations { get; }
    public Dictionary<ICreation, AuthorRole> GetCreations();
}
