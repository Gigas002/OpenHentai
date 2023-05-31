using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Database.Creatures;

[Table("characters")]
public class Character : Creature, ICharacter
{
    public IEnumerable<CreationsCharacters> CreationsCharacters { get; set; } = null!;

    public Dictionary<ICreation, CharacterRole> GetCreations() =>
        CreationsCharacters.ToDictionary(cc => (ICreation)cc.Creation, cc => cc.Role);

    public void SetCreations(Dictionary<Creation, CharacterRole> creations)
    {
        CreationsCharacters = creations.Select(creation => new CreationsCharacters()
        {
            Character = this,
            Creation = creation.Key,
            Role = creation.Value
        }).ToList();
    }
}
