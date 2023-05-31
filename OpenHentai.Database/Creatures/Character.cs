using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;

namespace OpenHentai.Database.Creatures;

[Table("characters")]
public class Character : Creature //, ICharacter
{
    public List<CharactersCreations> InCreations { get; init; } = new List<CharactersCreations>();
}
