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
public class Author : Creature//, IAuthor
{
    #region Properties
    
    public HashSet<AuthorsNames> AuthorsNames { get; init; } = new();

    /// <inheritdoc />
    public HashSet<Circle> Circles { get; init; } = new();

    /// <inheritdoc />
    [Column(TypeName = "jsonb")]
    public HashSet<ExternalLinkInfo> ExternalLinks { get; init; } = new();

    public HashSet<AuthorsCreations> AuthorsCreations { get; init; } = new();
    
    #endregion

    #region Methods

    public IEnumerable<LanguageSpecificTextInfo> GetAuthorNames() =>
        AuthorsNames.Select(an => an.GetLanguageSpecificTextInfo());

    public void AddAuthorNames(IEnumerable<LanguageSpecificTextInfo> names) =>
        names.ToList().ForEach(AddName);
    
    public void AddAuthorName(LanguageSpecificTextInfo name) => AuthorsNames.Add(new(this, name));

    public Dictionary<Creation, AuthorRole> GetCreations() =>
        AuthorsCreations.ToDictionary(ac => ac.Creation, ac => ac.Role);

    public void AddCreations(Dictionary<Creation, AuthorRole> creations) =>
        creations.ToList().ForEach(AddCreation);
    
    public void AddCreation(KeyValuePair<Creation, AuthorRole> creation) =>
        AddCreation(creation.Key, creation.Value);

    public void AddCreation(Creation creation, AuthorRole role) =>
        AuthorsCreations.Add(new(this, creation, role));
    
    #endregion
}
