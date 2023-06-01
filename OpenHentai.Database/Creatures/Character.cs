using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Database.Creatures;

[Table("characters")]
public class Character : Creature//, ICharacter
{
    #region Properties

    public HashSet<CreationsCharacters> CreationsCharacters { get; init; } = new();

    #endregion

    #region Methods

    public Dictionary<ICreation, CharacterRole> GetCreations() =>
        CreationsCharacters.ToDictionary(cc => (ICreation)cc.Creation, cc => cc.Role);

    public void AddCreations(Dictionary<Creation, CharacterRole> creations) =>
        creations.ToList().ForEach(AddCreation);
    
    public void AddCreation(KeyValuePair<Creation, CharacterRole> creation) =>
        AddCreation(creation.Key, creation.Value);

    public void AddCreation(Creation creation, CharacterRole role) =>
        CreationsCharacters.Add(new(creation, this, role));

    #endregion
}
