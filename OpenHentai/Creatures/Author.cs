using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Relative;
using OpenHentai.Descriptors;
using OpenHentai.Roles;
using System.Text.Json.Serialization;
using OpenHentai.JsonConverters;

namespace OpenHentai.Creatures;

/// <summary>
/// Author
/// </summary>
[Table("authors")]
public class Author : Creature
{
    #region Properties
    
    /// <summary>
    /// Main name must be romanization of native name (e.g. Hepburn romanization for ja-JP)
    /// Alternative names can be any
    /// </summary>
    public HashSet<AuthorsNames> AuthorsNames { get; init; } = new();

    /// <summary>
    /// Author's circles
    /// </summary>
    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Circle>))]
    public HashSet<Circle> Circles { get; init; } = new();

    /// <summary>
    /// Links to author's social networks, e.g. twitter, pixiv, fanbox, ci-en, etc
    /// </summary>
    [Column(TypeName = "jsonb")]
    public HashSet<ExternalLinkInfo> ExternalLinks { get; init; } = new();

    /// <summary>
    /// Collection of author works
    /// </summary>
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
