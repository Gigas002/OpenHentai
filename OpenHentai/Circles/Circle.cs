using OpenHentai.Creatures;
using OpenHentai.Creations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Relative;
using OpenHentai.Circles;
using OpenHentai.Descriptors;

namespace OpenHentai.Circles;

[Table("circles")]
public class Circle : IDatabaseEntity
{
    #region Properties

    /// <inheritdoc />
    public ulong Id { get; init; }

    public HashSet<CirclesTitles> CirclesTitles { get; init; } = new();

    public HashSet<Author> Authors { get; init; } = new();

    public HashSet<Creation> Creations { get; init; } = new();
    
    #endregion

    #region Methods
    
    public IEnumerable<LanguageSpecificTextInfo> GetTitles() =>
        CirclesTitles.Select(t => t.GetLanguageSpecificTextInfo());

    public void AddTitles(IEnumerable<LanguageSpecificTextInfo> titles) =>
        titles.ToList().ForEach(AddTitle);
    
    public void AddTitle(LanguageSpecificTextInfo title) => CirclesTitles.Add(new(this, title));
    
    #endregion
}
