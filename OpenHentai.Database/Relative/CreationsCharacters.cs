using Microsoft.EntityFrameworkCore;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Creatures;
using OpenHentai.Roles;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenHentai.Database.Relative;

[Table("creations_characters")]
[PrimaryKey("creation_id", "character_id")]
public class CreationsCharacters
{
    [ForeignKey("creation_id")]
    public Creation Creation { get; set; }

    [ForeignKey("character_id")]
    public Character Character { get; set; }

    public CharacterRole CharacterRole { get; set; }
}