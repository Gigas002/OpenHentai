using Microsoft.EntityFrameworkCore;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.JsonConverters;
using OpenHentai.Roles;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OpenHentai.Relative;

[PrimaryKey("creation_id", "character_id")]
public class CreationsCharacters
{
    #region Properties

    [ForeignKey("creation_id")]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Creation>))]
    public Creation Creation { get; set; } = null!;

    [ForeignKey("character_id")]
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
