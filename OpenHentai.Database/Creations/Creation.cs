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
public class Creation : IDatabaseEntity, ICreation
{
    public ulong Id { get; set; }

    private IEnumerable<CreationsTitles> CreationsTitles { get; set; } = null!;

    public IEnumerable<AuthorsCreations> AuthorsCreations { get; set; } = null!;

    public IEnumerable<Circle>? Circles { get; set; }

    public DateTime? PublishStarted { get; set; }

    public DateTime? PublishEnded { get; set; }

    [Column(TypeName = "jsonb")]
    public IEnumerable<ExternalLinkInfo>? Sources { get; set; }

    [Column(TypeName = "jsonb")]
    public IEnumerable<LanguageSpecificTextInfo>? Description { get; set; }

    public IEnumerable<CreationsRelations>? CreationsRelations { get; set; }

    [NotMapped]
    public IEnumerable<ICreationCollection> Collections { get; set; }

    public IEnumerable<CreationsCharacters>? CreationsCharacters { get; set; }

    public IEnumerable<MediaInfo>? Media { get; set; }

    [Column(TypeName = "jsonb")]
    public IEnumerable<LanguageInfo> Languages { get; set; } = null!;

    public Rating Rating { get; set; }

    public PublishStatus Status { get; set; }

    [Column(TypeName = "jsonb")]
    public IEnumerable<CensorshipInfo>? Censorship { get; set; }

    public IEnumerable<Tag> Tags { get; set; } = null!;

    public Dictionary<IAuthor, AuthorRole> GetAuthors() =>
        AuthorsCreations.ToDictionary(ac => (IAuthor)ac.Author, ac => ac.Role);

    public void SetAuthors(Dictionary<Author, AuthorRole> authors)
    {
        AuthorsCreations = authors.Select(author => new AuthorsCreations()
        {
            Creation = this,
            Author = author.Key,
            Role = author.Value
        }).ToList();
    }

    public Dictionary<ICharacter, CharacterRole> GetCharacters() =>
        CreationsCharacters.ToDictionary(cc => (ICharacter)cc.Character, cc => cc.Role);

    public void SetCharacters(Dictionary<Character, CharacterRole> characters)
    {
        CreationsCharacters = characters.Select(character => new CreationsCharacters()
        {
            Creation = this,
            Character = character.Key,
            Role = character.Value
        }).ToList();
    }

    public Dictionary<ICreation, CreationRelations> GetRelations() =>
        CreationsRelations.ToDictionary(cr => (ICreation)cr.Creation, cr => cr.Relation);

    public void SetRelations(Dictionary<Creation, CreationRelations> relations)
    {
        CreationsRelations = relations.Select(relation => new CreationsRelations()
        {
            Creation = this,
            RelatedCreation = relation.Key,
            Relation = relation.Value
        }).ToList();
    }

    public IEnumerable<ITag> GetTags() => Tags;

    public IEnumerable<LanguageSpecificTextInfo> GetTitles() =>
        CreationsTitles.Select(t => t.GetLanguageSpecificTextInfo());

    public void SetTitles(IEnumerable<LanguageSpecificTextInfo> titles) =>
        CreationsTitles = titles.Select(t => new CreationsTitles(this, t));
}
