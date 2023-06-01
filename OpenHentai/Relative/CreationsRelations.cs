using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Creations;
using OpenHentai.Relations;

namespace OpenHentai.Relative;

[Table("creations_relations")]
[PrimaryKey("creation_id", "related_creation_id")]
public class CreationsRelations
{
    #region Properties

    [ForeignKey("creation_id")]
    public Creation Creation { get; set; } = null!;

    [ForeignKey("related_creation_id")]
    public Creation RelatedCreation { get; set; } = null!;

    public CreationRelations Relation { get; set; }

    #endregion

    #region Constructors

    public CreationsRelations() { }
    
    public CreationsRelations(Creation creation, Creation relatedCreation, CreationRelations relation) =>
        (Creation, RelatedCreation, Relation) = (creation, relatedCreation, relation);

    #endregion
}
