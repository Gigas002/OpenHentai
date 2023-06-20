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
    public ulong Id { get; set; }

    /// <summary>
    /// Main name must be romanization of native name (e.g. Hepburn romanization for ja-JP)
    /// Alternative names can be any
    /// </summary>
    [JsonIgnore]
    public HashSet<CreaturesNames> Names { get; init; } = new();

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
    public HashSet<CreaturesRelations> Relations { get; init; } = new();
    
    #endregion

    #region Constructors

    public Creature() { }

    public Creature(ulong id) : this() => Id = id;

    public Creature(LanguageSpecificTextInfo name) : this() => AddName(name);

    public Creature(string formattedName) : this(new LanguageSpecificTextInfo(formattedName)) { }

    #endregion

    #region Methods

    public IEnumerable<LanguageSpecificTextInfo> GetNames() =>
        Names.Select(n => n.GetLanguageSpecificTextInfo());

    public void AddNames(IEnumerable<LanguageSpecificTextInfo> names) =>
        names.ToList().ForEach(AddName);
    
    public void AddName(LanguageSpecificTextInfo name) => Names.Add(new(this, name));

    public void AddName(string formattedName) =>
        AddName(new LanguageSpecificTextInfo(formattedName));
    
    public Dictionary<Creature, CreatureRelations> GetRelations() =>
        Relations.ToDictionary(cr => cr.Related, cr => cr.Relation);

    public void AddRelations(Dictionary<Creature, CreatureRelations> relations) =>
        relations.ToList().ForEach(AddRelation);
    
    public void AddRelation(KeyValuePair<Creature, CreatureRelations> relation) =>
        AddRelation(relation.Key, relation.Value);

    public void AddRelation(Creature relatedCreature, CreatureRelations relation) =>
        Relations.Add(new(this, relatedCreature, relation));
    
    #endregion
}
