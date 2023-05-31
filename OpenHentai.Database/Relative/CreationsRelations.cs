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
    public Creation Creation { get; set; }

    [ForeignKey("related_creation_id")]
    public Creation RelatedCreation { get; set; }

    public CreationRelations Relation { get; set; }
}
