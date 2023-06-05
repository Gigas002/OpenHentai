using Microsoft.EntityFrameworkCore;
using OpenHentai.Constants;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.JsonConverters;
using OpenHentai.Roles;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OpenHentai.Relative;

[PrimaryKey(FieldNames.CreationId, FieldNames.CharacterId)]
public class CreationsCharacters : IRelativeDatabaseEntity<Creation, Character, CharacterRole>
{
    #region Properties

    [ForeignKey(FieldNames.CreationId)]
    [JsonPropertyName(FieldNames.CreationId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Creation>))]
    public Creation Origin { get; set; } = null!;

    [ForeignKey(FieldNames.CharacterId)]
    [JsonPropertyName(FieldNames.CharacterId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Character>))]
    public Character Related { get; set; } = null!;

    public CharacterRole Relation { get; set; }
    
    #endregion

    #region Constructors

    public CreationsCharacters() { }

    public CreationsCharacters(Creation creation, Character character, CharacterRole role) =>
        (Origin, Related, Relation) = (creation, character, role);

    #endregion
}
