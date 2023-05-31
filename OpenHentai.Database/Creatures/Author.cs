using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Database.Circles;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Relative;
using OpenHentai.Descriptors;
using OpenHentai.Roles;

namespace OpenHentai.Database.Creatures;

/// <inheritdoc />
[Table("authors")]
public class Author : Creature, IAuthor
{
    #region Properties

    #region Interfaces implementation

    public IEnumerable<AuthorsNames> AuthorsNames { get; set; } = null!;

    /// <inheritdoc />
    public IEnumerable<Circle>? Circles { get; set; }

    /// <inheritdoc />
    [Column(TypeName = "jsonb")]
    public IEnumerable<ExternalLinkInfo>? ExternalLinks { get; set; }

    public IEnumerable<AuthorsCreations>? AuthorsCreations { get; set; }

    public IEnumerable<LanguageSpecificTextInfo> GetAuthorNames() =>
        AuthorsNames.Select(an => an.GetLanguageSpecificTextInfo());

    public void SetAuthorNames(IEnumerable<LanguageSpecificTextInfo> names) =>
        AuthorsNames = names.Select(n => new AuthorsNames(this, n));

    public IEnumerable<ICircle> GetCircles() => Circles;

    public Dictionary<ICreation, AuthorRole> GetCreations() =>
        AuthorsCreations.ToDictionary(ac => (ICreation)ac.Creation, ac => ac.Role);

    public void SetCreations(Dictionary<Creation, AuthorRole> creations)
    {
        AuthorsCreations = creations.Select(creation => new AuthorsCreations()
        {
            Author = this,
            Creation = creation.Key,
            Role = creation.Value
        }).ToList();
    }

    #endregion

    #endregion
}
