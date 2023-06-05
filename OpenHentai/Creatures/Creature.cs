using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.Relations;
using OpenHentai.Tags;
using System.Text.Json.Serialization;
using OpenHentai.JsonConverters;
using OpenHentai.Constants;

namespace OpenHentai.Creatures;

/// <summary>
/// Creature
/// </summary>
[Table(TableNames.Creatures)]
public class Creature : IDatabaseEntity
{
    #region Properties

    /// <inheritdoc />
    public ulong Id { get; init; }

    /// <summary>
    /// Main name must be romanization of native name (e.g. Hepburn romanization for ja-JP)
    /// Alternative names can be any
    /// </summary>
    public HashSet<CreaturesNames> CreaturesNames { get; init; } = new();

    /// <summary>
    /// Description, e.g. this person is a dick
    /// </summary>
    [Column(TypeName = DataTypes.Jsonb)]
    public HashSet<LanguageSpecificTextInfo> Description { get; init; } = new();

    /// <summary>
    /// Creature's birthday, e.g. 01.01.1922
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// Creature's age, e.g. 500
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Collection of related pictures
    /// </summary>
    public HashSet<MediaInfo> Media { get; init; } = new();

    /// <summary>
    /// Creature's gender
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Creature's additional details/tags
    /// </summary>
    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Tag>))]
    public HashSet<Tag> Tags { get; init; } = new();

    /// <summary>
    /// Collection of related and alternative creatures,
    /// Creature-Relation pair, e.g. "Admiral, alternative"
    /// </summary>
    public HashSet<CreaturesRelations> CreaturesRelations { get; init; } = new();
    
    #endregion

    #region Constructors

    public Creature() { }

    public Creature(ulong id) => Id = id;

    #endregion

    #region Methods

    public IEnumerable<LanguageSpecificTextInfo> GetNames() =>
        CreaturesNames.Select(n => n.GetLanguageSpecificTextInfo());

    public void AddNames(IEnumerable<LanguageSpecificTextInfo> names) =>
        names.ToList().ForEach(AddName);
    
    public void AddName(LanguageSpecificTextInfo name) => CreaturesNames.Add(new(this, name));
    
    public Dictionary<Creature, CreatureRelations> GetRelations() =>
        CreaturesRelations.ToDictionary(cr => cr.Related, cr => cr.Relation);

    public void AddRelations(Dictionary<Creature, CreatureRelations> relations) =>
        relations.ToList().ForEach(AddRelation);
    
    public void AddRelation(KeyValuePair<Creature, CreatureRelations> relation) =>
        AddRelation(relation.Key, relation.Value);

    public void AddRelation(Creature relatedCreature, CreatureRelations relation) =>
        CreaturesRelations.Add(new(this, relatedCreature, relation));
    
    #endregion
}
