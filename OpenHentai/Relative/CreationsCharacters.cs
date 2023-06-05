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
public class CreationsCharacters
{
    #region Properties

    [ForeignKey(FieldNames.CreationId)]
    [JsonPropertyName(FieldNames.CreationId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Creation>))]
    public Creation Creation { get; set; } = null!;

    [ForeignKey(FieldNames.CharacterId)]
    [JsonPropertyName(FieldNames.CharacterId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Character>))]
    public Character Character { get; set; } = null!;

    public CharacterRole Role { get; set; }
    
    #endregion

    #region Constructors

    public CreationsCharacters() { }

    public CreationsCharacters(Creation creation, Character character, CharacterRole role) =>
        (Creation, Character, Role) = (creation, character, role);

    #endregion
}
