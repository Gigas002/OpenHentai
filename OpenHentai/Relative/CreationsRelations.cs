using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Constants;
using OpenHentai.Creations;
using OpenHentai.JsonConverters;
using OpenHentai.Relations;

namespace OpenHentai.Relative;

[PrimaryKey(FieldNames.CreationId, FieldNames.RelatedCreationId)]
public class CreationsRelations : IRelativeDatabaseEntity<Creation, Creation, CreationRelations>
{
    #region Properties

    [ForeignKey(FieldNames.CreationId)]
    [JsonPropertyName(FieldNames.CreationId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Creation>))]
    public Creation Origin { get; set; } = null!;

    [ForeignKey(FieldNames.RelatedCreationId)]
    [JsonPropertyName(FieldNames.RelatedCreationId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Creation>))]
    public Creation Related { get; set; } = null!;

    public CreationRelations Relation { get; set; }

    #endregion

    #region Constructors

    public CreationsRelations() { }
    
    public CreationsRelations(Creation creation, Creation relatedCreation, CreationRelations relation) =>
        (Origin, Related, Relation) = (creation, relatedCreation, relation);

    #endregion
}
