using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Database.Tags;
using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;
using OpenHentai.Relations;
using OpenHentai.Tags;

namespace OpenHentai.Database.Creatures;

[Table("creatures")]
public class Creature : IDatabaseEntity, ICreature
{
    /// <inheritdoc />
    public ulong Id { get; set; }

    /// <inheritdoc />
    public IEnumerable<CreaturesNames> CreaturesNames { get; set; } = null!;

    /// <inheritdoc />
    [Column(TypeName = "jsonb")]
    public IEnumerable<LanguageSpecificTextInfo>? Description { get; set; }

    /// <inheritdoc />
    public DateTime? Birthday { get; set; }

    /// <inheritdoc />
    public int Age { get; set; }

    public IEnumerable<MediaInfo>? Media { get; set; }

    /// <inheritdoc />
    public Gender Gender { get; set; }

    /// <inheritdoc />
    public IEnumerable<Tag> Tags { get; set; } = null!;

    public IEnumerable<CreaturesRelations>? CreaturesRelations { get; set; }

    public IEnumerable<LanguageSpecificTextInfo> GetNames() =>
        CreaturesNames.Select(n => n.GetLanguageSpecificTextInfo());

    public void SetNames(IEnumerable<LanguageSpecificTextInfo> names) =>
        // TODO: for unknown reasons, this REQUIRES ToList()
        CreaturesNames = names.Select(n => new CreaturesNames(this, n)).ToList();

    public Dictionary<ICreature, CreatureRelations> GetRelations() =>
        CreaturesRelations.ToDictionary(cr => (ICreature)cr.Creature, cr => cr.Relation);

    public void SetRelations(Dictionary<Creature, CreatureRelations> relations)
    {
        CreaturesRelations = relations.Select(relation => new CreaturesRelations()
        {
            Creature = this,
            RelatedCreature = relation.Key,
            Relation = relation.Value
        }).ToList();
    }

    public IEnumerable<ITag> GetTags() => Tags;
}
