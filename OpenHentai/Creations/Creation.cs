using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Circles;
using OpenHentai.Descriptors;
using OpenHentai.Statuses;
using OpenHentai.Tags;
using OpenHentai.Relative;
using OpenHentai.Creatures;
using OpenHentai.Roles;
using OpenHentai.Relations;

namespace OpenHentai.Creations;

/// <summary>
/// Creation, e.g. doujinshi, manga, etc
/// </summary>
[Table("creations")]
public abstract class Creation : IDatabaseEntity
{
    #region Properties

    /// <inheritdoc />
    public ulong Id { get; init; }

    /// <summary>
    /// Main title must be romanization of native title (e.g. Hepburn romanization for ja-JP)
    /// Alternative titles can be any
    /// e.g. "ja-JP:ポプテピピック;en-US:Pop team epic"
    /// </summary>
    public HashSet<CreationsTitles> CreationsTitles { get; init; } = new();

    /// <summary>
    /// Authors and their roles
    /// </summary>
    public HashSet<AuthorsCreations> AuthorsCreations { get; init; } = new();

    /// <summary>
    /// Circles
    /// </summary>
    public HashSet<Circle> Circles { get; init; } = new();

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
    [Column(TypeName = "jsonb")]
    public HashSet<ExternalLinkInfo> Sources { get; init; } = new();

    /// <summary>
    /// Description
    /// </summary>
    [Column(TypeName = "jsonb")]
    public HashSet<LanguageSpecificTextInfo> Description { get; init; } = new();

    /// <summary>
    /// Collection of related creations
    /// Creation-Relation pair
    /// </summary>
    public HashSet<CreationsRelations> CreationsRelations { get; init; } = new();

    // /// <summary>
    // /// Featured at events, e.g. C99, C100, etc
    // /// </summary>
    // public IEnumerable<IEvent> Events { get; set; }

    // /// <summary>
    // /// Member of collections
    // /// </summary>
    // TODO: this
    // [NotMapped]
    // public IEnumerable<ICreationCollection> Collections { get; set; }

    /// <summary>
    /// Collection of characters
    /// </summary>
    public HashSet<CreationsCharacters> CreationsCharacters { get; init; } = new();

    /// <summary>
    /// Collection of related media, including preview image
    /// </summary>
    public HashSet<MediaInfo> Media { get; init; } = new();

    /// <summary>
    /// Available on languages
    /// </summary>
    [Column(TypeName = "jsonb")]
    public HashSet<LanguageInfo> Languages { get; init; } = new();

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
    [Column(TypeName = "jsonb")]
    public HashSet<CensorshipInfo> Censorship { get; init; } = new();

    /// <summary>
    /// Creation's tags
    /// e.g. franchise parody, themes, etc
    /// </summary>
    public HashSet<Tag> Tags { get; init; } = new();
    
    #endregion

    #region Methods
    
    public IEnumerable<LanguageSpecificTextInfo> GetTitles() =>
        CreationsTitles.Select(t => t.GetLanguageSpecificTextInfo());

    public void AddTitles(IEnumerable<LanguageSpecificTextInfo> titles) =>
        titles.ToList().ForEach(AddTitle);
    
    public void AddTitle(LanguageSpecificTextInfo title) => CreationsTitles.Add(new(this, title));

    public Dictionary<Author, AuthorRole> GetAuthors() =>
        AuthorsCreations.ToDictionary(ac => ac.Author, ac => ac.Role);

    public void AddAuthors(Dictionary<Author, AuthorRole> authors) =>
        authors.ToList().ForEach(AddAuthor);
    
    public void AddAuthor(KeyValuePair<Author, AuthorRole> author) =>
        AddAuthor(author.Key, author.Value);
    
    public void AddAuthor(Author author, AuthorRole role) =>
        AuthorsCreations.Add(new(author, this, role));

    public Dictionary<Creation, CreationRelations> GetRelations() =>
        CreationsRelations.ToDictionary(cr => cr.RelatedCreation, cr => cr.Relation);

    public void AddRelations(Dictionary<Creation, CreationRelations> relations) =>
        relations.ToList().ForEach(AddRelation);
    
    public void AddRelation(KeyValuePair<Creation, CreationRelations> relation) =>
        AddRelation(relation.Key, relation.Value);

    public void AddRelation(Creation relatedCreation, CreationRelations relation) =>
        CreationsRelations.Add(new(this, relatedCreation, relation));
    
    public Dictionary<Character, CharacterRole> GetCharacters() =>
        CreationsCharacters.ToDictionary(cc => cc.Character, cc => cc.Role);

    public void AddCharacters(Dictionary<Character, CharacterRole> characters) =>
        characters.ToList().ForEach(AddCharacter);
    
    public void AddCharacter(KeyValuePair<Character, CharacterRole> character) =>
        AddCharacter(character.Key, character.Value);

    public void AddCharacter(Character character, CharacterRole role) =>
        CreationsCharacters.Add(new(this, character, role));

    #endregion
}


// colletions through relations:
// creation_1 = manga vol1
// creation_2 = manga vol2
// creation_col = manga full (vol1, vol2)

// creation_1 relations: creation_2--parent; creation_col--slave
// creation_2 relations: creation_1--child; creation_col--slave
// creation_col relations: creation_1--master; creation_2--master