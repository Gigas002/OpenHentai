using OpenHentai.Database.Creatures;
using OpenHentai.Database.Creations;
using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;
using OpenHentai.Circles;
using OpenHentai.Creatures;
using OpenHentai.Creations;
using OpenHentai.Descriptors;

namespace OpenHentai.Database.Circles;

/// <inheritdoc cref="ICircle"/>
[Table("circles")]
public class Circle : IDatabaseEntity//, ICircle
{
    #region Properties

    /// <inheritdoc />
    public ulong Id { get; init; }

    public HashSet<CirclesTitles> CirclesTitles { get; } = new();

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
