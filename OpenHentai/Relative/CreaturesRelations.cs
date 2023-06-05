using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Constants;
using OpenHentai.Creatures;
using OpenHentai.JsonConverters;
using OpenHentai.Relations;

namespace OpenHentai.Relative;

[PrimaryKey(FieldNames.CreatureId, FieldNames.RelatedCreatureId)]
public class CreaturesRelations
{
    #region Properties

    [ForeignKey(FieldNames.CreatureId)]
    [JsonPropertyName(FieldNames.CreatureId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Creature>))]
    public Creature Creature { get; set; } = null!;

    [ForeignKey(FieldNames.RelatedCreatureId)]
    [JsonPropertyName(FieldNames.RelatedCreatureId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Creature>))]
    public Creature RelatedCreature { get; set; } = null!;

    public CreatureRelations Relation { get; set; }

    #endregion

    #region Constructors

    public CreaturesRelations() { }

    public CreaturesRelations(Creature creature, Creature relatedCreature, CreatureRelations relation) =>
        (Creature, RelatedCreature, Relation) = (creature, relatedCreature, relation);

    #endregion
}
