using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Constants;
using OpenHentai.Creations;
using OpenHentai.Relative;
using OpenHentai.Roles;

namespace OpenHentai.Creatures;

/// <summary>
/// Character
/// </summary>
[Table(TableNames.Characters)]
public class Character : Creature
{
    #region Properties

    /// <summary>
    /// Collection of creations, that has this character
    /// </summary>
    public HashSet<CreationsCharacters> CreationsCharacters { get; init; } = new();

    #endregion

    #region Methods

    public Dictionary<Creation, CharacterRole> GetCreations() =>
        CreationsCharacters.ToDictionary(cc => cc.Origin, cc => cc.Relation);

    public void AddCreations(Dictionary<Creation, CharacterRole> creations) =>
        creations.ToList().ForEach(AddCreation);
    
    public void AddCreation(KeyValuePair<Creation, CharacterRole> creation) =>
        AddCreation(creation.Key, creation.Value);

    public void AddCreation(Creation creation, CharacterRole role) =>
        CreationsCharacters.Add(new(creation, this, role));

    #endregion
}
