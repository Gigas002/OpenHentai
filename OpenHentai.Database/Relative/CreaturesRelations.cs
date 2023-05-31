using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Database.Creatures;
using OpenHentai.Relations;

namespace OpenHentai.Database.Relative;

[Table("creatures_relations")]
[PrimaryKey("creature_id", "related_creature_id")]
public class CreaturesRelations
{
    [ForeignKey("creature_id")]
    public Creature Creature { get; set; } = null!;

    [ForeignKey("related_creature_id")]
    public Creature RelatedCreature { get; set; } = null!;

    public CreatureRelations Relation { get; set; }
    
    public CreaturesRelations() { }
    
    public CreaturesRelations(Creature creature, Creature relatedCreature, CreatureRelations relation) =>
        (Creature, RelatedCreature, Relation) = (creature, relatedCreature, relation);
}
