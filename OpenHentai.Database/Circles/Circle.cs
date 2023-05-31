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
public class Circle : IDatabaseEntity, ICircle
{
    /// <inheritdoc />
    public ulong Id { get; set; }

    public IEnumerable<CirclesTitles> Titles { get; set; } = null!;

    public IEnumerable<Author> Authors { get; set; } = null!;

    public IEnumerable<Creation>? Creations { get; set; }

    /// <inheritdoc />
    public IEnumerable<IAuthor> GetAuthors() => Authors;

    public IEnumerable<ICreation> GetCreations() => Creations;

    public IEnumerable<LanguageSpecificTextInfo> GetTitles() =>
        Titles.Select(t => new LanguageSpecificTextInfo(new(t.Language), t.Text));
}
