using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;

namespace OpenHentai.Database.Creatures;

[Table("characters")]
public class Character : Creature //, ICharacter
{
    public List<CreationsCharacters> InCreations { get; init; } = new();
}
