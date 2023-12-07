using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Circles;
using OpenHentai.Descriptors;
using OpenHentai.Statuses;
using OpenHentai.Tags;
using OpenHentai.Relative;
using OpenHentai.Creatures;
using OpenHentai.Roles;
using OpenHentai.Relations;
using System.Text.Json.Serialization;
using OpenHentai.JsonConverters;
using OpenHentai.Constants;

namespace OpenHentai.Creations;

/// <summary>
/// Creation, e.g. doujinshi, manga, etc
/// </summary>
[Table(TableNames.Creations)]
public class Creation : IDatabaseEntity
{
    #region Properties

    /// <inheritdoc />
    public ulong Id { get; set; }

    /// <summary>
    /// Main title must be romanization of native title (e.g. Hepburn romanization for ja-JP)
    /// Alternative titles can be any
    /// e.g. "ja-JP:ポプテピピック;en-US:Pop team epic"
    /// </summary>
    [JsonIgnore]
    public HashSet<CreationsTitles> Titles { get; init; } = [];

    /// <summary>
    /// Authors and their roles
    /// </summary>
    public HashSet<AuthorsCreations> Authors { get; init; } = [];

    /// <summary>
    /// Circles
    /// </summary>
    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Circle>))]
    public HashSet<Circle> Circles { get; init; } = [];

    /// <summary>
    /// Estimate date of first release of this creation
    /// </summary>
    public DateTime? PublishStarted { get; set; }

    /// <summary>
    /// Estimate date of final release of this creation
    /// </summary>
    public DateTime? PublishEnded { get; set; }

    /// <summary>
    /// Available to purchase/read/etc at
    /// </summary>
    [Column(TypeName = DataTypes.Jsonb)]
    public HashSet<ExternalLinkInfo> Sources { get; init; } = [];

    /// <summary>
    /// Description
    /// </summary>
    [Column(TypeName = DataTypes.Jsonb)]
    public HashSet<LanguageSpecificTextInfo> Description { get; init; } = [];

    /// <summary>
    /// Collection of related creations
    /// Creation-Relation pair
    /// </summary>
    public HashSet<CreationsRelations> Relations { get; init; } = [];

    // /// <summary>
    // /// Featured at events, e.g. C99, C100, etc
    // /// </summary>
    // public IEnumerable<IEvent> Events { get; set; }

    /// <summary>
    /// Collection of characters
    /// </summary>
    public HashSet<CreationsCharacters> Characters { get; init; } = [];

    /// <summary>
    /// Collection of related media, including preview image
    /// </summary>
    [Column(TypeName = DataTypes.Jsonb)]
    public HashSet<MediaInfo> Media { get; init; } = [];

    /// <summary>
    /// Available on languages
    /// </summary>
    [Column(TypeName = DataTypes.Jsonb)]
    public HashSet<LanguageInfo> Languages { get; init; } = [];

    /// <summary>
    /// Age rating
    /// </summary>
    public Rating Rating { get; set; }

    /// <summary>
    /// Publishing status
    /// </summary>
    public PublishStatus Status { get; set; }

    /// <summary>
    /// Censorship type
    /// </summary>
    [Column(TypeName = DataTypes.Jsonb)]
    public HashSet<CensorshipInfo> Censorship { get; init; } = [];

    /// <summary>
    /// Creation's tags
    /// e.g. franchise parody, themes, etc
    /// </summary>
    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Tag>))]
    public HashSet<Tag> Tags { get; init; } = [];

    #endregion

    #region Constructors

    public Creation() { }

    public Creation(ulong id) => Id = id;

    public Creation(LanguageSpecificTextInfo title) => AddTitle(title);

    public Creation(string formattedTitle) : this(new LanguageSpecificTextInfo(formattedTitle)) { }

    #endregion

    #region Methods

    /// <summary>
    /// Convert relational database's object into collection of formatted objects
    /// </summary>
    public IEnumerable<LanguageSpecificTextInfo> GetTitles() =>
        Titles.Select(t => t.GetLanguageSpecificTextInfo());

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
    public void AddTitle(LanguageSpecificTextInfo title) => Titles.Add(new(this, title));

    public void AddTitle(string formattedTitle) =>
        AddTitle(new LanguageSpecificTextInfo(formattedTitle));

    /// <inheritdoc cref="GetTitles" />
    public Dictionary<Author, AuthorRole> GetAuthors() =>
        Authors.ToDictionary(ac => ac.Origin, ac => ac.Relation);

    public void AddAuthors(Dictionary<Author, AuthorRole> authors) =>
        authors.ToList().ForEach(AddAuthor);

    public void AddAuthor(KeyValuePair<Author, AuthorRole> author) =>
        AddAuthor(author.Key, author.Value);

    public void AddAuthor(Author author, AuthorRole role) =>
        Authors.Add(new(author, this, role));

    /// <inheritdoc cref="GetTitles" />
    public Dictionary<Creation, CreationRelations> GetRelations() =>
        Relations.ToDictionary(cr => cr.Related, cr => cr.Relation);

    public void AddRelations(Dictionary<Creation, CreationRelations> relations) =>
        relations.ToList().ForEach(AddRelation);

    public void AddRelation(KeyValuePair<Creation, CreationRelations> relation) =>
        AddRelation(relation.Key, relation.Value);

    public void AddRelation(Creation relatedCreation, CreationRelations relation) =>
        Relations.Add(new(this, relatedCreation, relation));

    /// <inheritdoc cref="GetTitles" />
    public Dictionary<Character, CharacterRole> GetCharacters() =>
        Characters.ToDictionary(cc => cc.Related, cc => cc.Relation);

    public void AddCharacters(Dictionary<Character, CharacterRole> characters) =>
        characters.ToList().ForEach(AddCharacter);

    public void AddCharacter(KeyValuePair<Character, CharacterRole> character) =>
        AddCharacter(character.Key, character.Value);

    public void AddCharacter(Character character, CharacterRole role) =>
        Characters.Add(new(this, character, role));

    #endregion
}
