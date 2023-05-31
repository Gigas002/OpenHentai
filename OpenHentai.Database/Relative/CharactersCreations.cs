using Microsoft.EntityFrameworkCore;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Creatures;
using OpenHentai.Roles;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenHentai.Database.Relative;

[Table("characters_creations")]
[PrimaryKey("character_id", "creation_id")]
public class CharactersCreations
{
    [ForeignKey("character_id")]
    public Character Character { get; set; }

    [ForeignKey("creation_id")]
    public Creation Creation { get; set; }

    public CharacterRole CharacterRole { get; set; }
}