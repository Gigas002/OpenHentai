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
    public Creature Creature { get; set; }

    [ForeignKey("related_creature_id")]
    public Creature RelatedCreature { get; set; }

    public CreatureRelations Relation { get; set; }
}
