using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Database.Tags;
using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;
using OpenHentai.Relations;
using OpenHentai.Tags;

namespace OpenHentai.Database.Creatures;

[Table("creatures")]
public abstract class Creature : IDatabaseEntity//, ICreature
{
    #region Properties

    /// <inheritdoc />
    public ulong Id { get; set; }

    /// <inheritdoc />
    public HashSet<CreaturesNames> CreaturesNames { get; init; } = new();

    /// <inheritdoc />
    [Column(TypeName = "jsonb")]
    public HashSet<LanguageSpecificTextInfo> Description { get; init; } = new();

    /// <inheritdoc />
    public DateTime? Birthday { get; set; }

    /// <inheritdoc />
    public int Age { get; set; }

    public HashSet<MediaInfo> Media { get; init; } = new();

    /// <inheritdoc />
    public Gender Gender { get; set; }

    /// <inheritdoc />
    public HashSet<Tag> Tags { get; init; } = new();

    public HashSet<CreaturesRelations> CreaturesRelations { get; init; } = new();
    
    #endregion

    #region Methods

    public IEnumerable<LanguageSpecificTextInfo> GetNames() =>
        CreaturesNames.Select(n => n.GetLanguageSpecificTextInfo());

    public void AddNames(IEnumerable<LanguageSpecificTextInfo> names) =>
        names.ToList().ForEach(AddName);
    
    public void AddName(LanguageSpecificTextInfo name) => CreaturesNames.Add(new(this, name));
    
    public Dictionary<ICreature, CreatureRelations> GetRelations() =>
        CreaturesRelations.ToDictionary(cr => (ICreature)cr.Creature, cr => cr.Relation);

    public void AddRelations(Dictionary<Creature, CreatureRelations> relations) =>
        relations.ToList().ForEach(AddRelation);
    
    public void AddRelation(KeyValuePair<Creature, CreatureRelations> relation) =>
        AddRelation(relation.Key, relation.Value);

    public void AddRelation(Creature relatedCreature, CreatureRelations relation) =>
        CreaturesRelations.Add(new(this, relatedCreature, relation));
    
    #endregion
}
