using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Creatures;
using OpenHentai.Creations;
using OpenHentai.Relative;
using OpenHentai.Descriptors;

namespace OpenHentai.Circles;

/// <summary>
/// Author's circle
/// </summary>
[Table("circles")]
public class Circle : IDatabaseEntity
{
    #region Properties

    /// <inheritdoc />
    public ulong Id { get; init; }

    /// <summary>
    /// Main title must be romanization of native title (e.g. Hepburn romanization for ja-JP)
    /// Alternative titles can be any
    /// e.g. "ja-JP:ポプテピピック;en-US:Pop team epic"
    /// </summary>
    public HashSet<CirclesTitles> CirclesTitles { get; init; } = new();

    /// <summary>
    /// Related authors
    /// </summary>
    public HashSet<Author> Authors { get; init; } = new();

    /// <summary>
    /// Related creations
    /// </summary>
    public HashSet<Creation> Creations { get; init; } = new();
    
    #endregion

    #region Methods

    /// <summary>
    /// Convert relational database's object into collection of formatted objects
    /// </summary>    
    public IEnumerable<LanguageSpecificTextInfo> GetTitles() =>
        CirclesTitles.Select(t => t.GetLanguageSpecificTextInfo());

    /// <summary>
    /// Add titles to the relational database
    /// </summary>   
    /// <param name="titles">Titles</param>
    public void AddTitles(IEnumerable<LanguageSpecificTextInfo> titles) =>
        titles.ToList().ForEach(AddTitle);
    
    /// <summary>
    /// Add title to the relational database
    /// </summary>
    /// <param name="title">Title</param>
    public void AddTitle(LanguageSpecificTextInfo title) => CirclesTitles.Add(new(this, title));
    
    #endregion
}
