using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Database.Creations;
using OpenHentai.Relations;

namespace OpenHentai.Database.Relative;

[Table("creations_relations")]
[PrimaryKey("creation_id", "related_creation_id")]
public class CreationsRelations
{
    [ForeignKey("creation_id")]
    public Creation Creation { get; set; } = null!;

    [ForeignKey("related_creation_id")]
    public Creation RelatedCreation { get; set; } = null!;

    public CreationRelations Relation { get; set; }
}