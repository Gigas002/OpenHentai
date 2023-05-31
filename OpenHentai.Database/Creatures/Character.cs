using OpenHentai.Creations;
using OpenHentai.Roles;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenHentai.Database.Creatures;

[Table("characters")]
public class Character : Creature //, ICharacter
{
    [NotMapped]
    public IDictionary<ICreation, CharacterRole> InCreations { get; set; }
}
