using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Relative;
using OpenHentai.Descriptors;
using OpenHentai.Roles;
using System.Text.Json.Serialization;
using OpenHentai.JsonConverters;
using OpenHentai.Constants;

namespace OpenHentai.Creatures;

/// <summary>
/// Author
/// </summary>
[Table(TableNames.Authors)]
public class Author : Creature
{
    #region Properties
    
    /// <summary>
    /// Main name must be romanization of native name (e.g. Hepburn romanization for ja-JP)
    /// Alternative names can be any
    /// </summary>
    [JsonIgnore]
    public HashSet<AuthorsNames> AuthorNames { get; init; } = [];

    /// <summary>
    /// Author's circles
    /// </summary>
    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Circle>))]
    public HashSet<Circle> Circles { get; init; } = [];

    /// <summary>
    /// Links to author's social networks, e.g. twitter, pixiv, fanbox, ci-en, etc
    /// </summary>
    [Column(TypeName = DataTypes.Jsonb)]
    public HashSet<ExternalLinkInfo> ExternalLinks { get; init; } = [];

    /// <summary>
    /// Collection of author works
    /// </summary>
    public HashSet<AuthorsCreations> Creations { get; init; } = [];
    
    #endregion

    #region Constructors

    public Author() : base() { }

    public Author(ulong id) : base(id) { }

    public Author(LanguageSpecificTextInfo authorName) => AddAuthorName(authorName);

    public Author(string formattedAuthorName) : this(new LanguageSpecificTextInfo(formattedAuthorName)) { }

    #endregion

    #region Methods

    public IEnumerable<LanguageSpecificTextInfo> GetAuthorNames() =>
        AuthorNames.Select(an => an.GetLanguageSpecificTextInfo());

    public void AddAuthorNames(IEnumerable<LanguageSpecificTextInfo> names) =>
        names.ToList().ForEach(AddAuthorName);
    
    public void AddAuthorName(LanguageSpecificTextInfo name) => AuthorNames.Add(new(this, name));

    public void AddAuthorName(string formattedAuthorName) =>
        AddAuthorName(new LanguageSpecificTextInfo(formattedAuthorName));

    public Dictionary<Creation, AuthorRole> GetCreations() =>
        Creations.ToDictionary(ac => ac.Related, ac => ac.Relation);

    public void AddCreations(Dictionary<Creation, AuthorRole> creations) =>
        creations.ToList().ForEach(AddCreation);
    
    public void AddCreation(KeyValuePair<Creation, AuthorRole> creation) =>
        AddCreation(creation.Key, creation.Value);

    public void AddCreation(Creation creation, AuthorRole role) =>
        Creations.Add(new(this, creation, role));

    #endregion
}
