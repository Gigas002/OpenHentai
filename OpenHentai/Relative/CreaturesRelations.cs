using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Creatures;
using OpenHentai.Relations;

namespace OpenHentai.Relative;

[PrimaryKey("creature_id", "related_creature_id")]
public class CreaturesRelations
{
    #region Properties

    [ForeignKey("creature_id")]
    public Creature Creature { get; set; } = null!;

    [ForeignKey("related_creature_id")]
    public Creature RelatedCreature { get; set; } = null!;

    public CreatureRelations Relation { get; set; }
    
    #endregion

    #region Constructors

    public CreaturesRelations() { }
    
    public CreaturesRelations(Creature creature, Creature relatedCreature, CreatureRelations relation) =>
        (Creature, RelatedCreature, Relation) = (creature, relatedCreature, relation);

    #endregion
}
