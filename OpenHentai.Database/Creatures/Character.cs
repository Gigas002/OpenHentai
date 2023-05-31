using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Database.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Database.Creatures;

[Table("characters")]
public class Character : Creature, ICharacter
{
    public IEnumerable<CreationsCharacters> InCreations { get; set; } = null!;

    public Dictionary<ICreation, CharacterRole> GetInCreations()
    {
        throw new NotImplementedException();
    }
}
