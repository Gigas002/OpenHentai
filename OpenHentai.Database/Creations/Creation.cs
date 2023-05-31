using OpenHentai.Database.Circles;
using OpenHentai.Descriptors;
using OpenHentai.Statuses;
using OpenHentai.Database.Tags;
using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Roles;
using OpenHentai.Relations;
using OpenHentai.Tags;
using OpenHentai.Database.Creatures;

namespace OpenHentai.Database.Creations;

[Table("creations")]
public class Creation : IDatabaseEntity//, ICreation
{
    #region Properties

    public ulong Id { get; init; }

    public HashSet<CreationsTitles> CreationsTitles { get; init; } = new();

    public HashSet<AuthorsCreations> AuthorsCreations { get; init; } = new();

    public HashSet<Circle> Circles { get; init; } = new();

    public DateTime? PublishStarted { get; set; }

    public DateTime? PublishEnded { get; set; }

    [Column(TypeName = "jsonb")]
    public HashSet<ExternalLinkInfo> Sources { get; init; } = new();

    [Column(TypeName = "jsonb")]
    public HashSet<LanguageSpecificTextInfo> Description { get; init; } = new();

    public HashSet<CreationsRelations> CreationsRelations { get; init; } = new();

    // TODO: this
    [NotMapped]
    public IEnumerable<ICreationCollection> Collections { get; set; }

    public HashSet<CreationsCharacters>? CreationsCharacters { get; init; } = new();

    public HashSet<MediaInfo> Media { get; init; } = new();

    [Column(TypeName = "jsonb")]
    public HashSet<LanguageInfo> Languages { get; init; } = new();

    public Rating Rating { get; set; }

    public PublishStatus Status { get; set; }

    [Column(TypeName = "jsonb")]
    public HashSet<CensorshipInfo> Censorship { get; init; } = new();

    public HashSet<Tag> Tags { get; init; } = new();
    
    #endregion

    #region Methods
    
    public IEnumerable<LanguageSpecificTextInfo> GetTitles() =>
        CreationsTitles.Select(t => t.GetLanguageSpecificTextInfo());

    public void AddTitles(IEnumerable<LanguageSpecificTextInfo> titles) =>
        titles.ToList().ForEach(AddTitle);
    
    public void AddTitle(LanguageSpecificTextInfo title) => CreationsTitles.Add(new(this, title));

    public Dictionary<IAuthor, AuthorRole> GetAuthors() =>
        AuthorsCreations.ToDictionary(ac => (IAuthor)ac.Author, ac => ac.Role);

    public void AddAuthors(Dictionary<Author, AuthorRole> authors) =>
        authors.ToList().ForEach(AddAuthor);
    
    public void AddAuthor(KeyValuePair<Author, AuthorRole> author) =>
        AddAuthor(author.Key, author.Value);
    
    public void AddAuthor(Author author, AuthorRole role) =>
        AuthorsCreations.Add(new(author, this, role));

    public Dictionary<ICreation, CreationRelations> GetRelations() =>
        CreationsRelations.ToDictionary(cr => (ICreation)cr.Creation, cr => cr.Relation);

    public void AddRelations(Dictionary<Creation, CreationRelations> relations) =>
        relations.ToList().ForEach(AddRelation);
    
    public void AddRelation(KeyValuePair<Creation, CreationRelations> relation) =>
        AddRelation(relation.Key, relation.Value);

    public void AddRelation(Creation relatedCreation, CreationRelations relation) =>
        CreationsRelations.Add(new(this, relatedCreation, relation));
    
    public Dictionary<ICharacter, CharacterRole> GetCharacters() =>
        CreationsCharacters.ToDictionary(cc => (ICharacter)cc.Character, cc => cc.Role);

    public void AddCharacters(Dictionary<Character, CharacterRole> characters) =>
        characters.ToList().ForEach(AddCharacter);
    
    public void AddCharacter(KeyValuePair<Character, CharacterRole> character) =>
        AddCharacter(character.Key, character.Value);

    public void AddCharacter(Character character, CharacterRole role) =>
        CreationsCharacters.Add(new(this, character, role));

    // public IEnumerable<ITag> GetTags() => Tags;

    #endregion
}
