using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Constants;
using OpenHentai.Creations;
using OpenHentai.Descriptors;
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
    public HashSet<CreationsCharacters> Creations { get; init; } = new();

    #endregion

    #region Constructors

    public Character() : base() { }

    public Character(ulong id) : base(id) { }

    public Character(LanguageSpecificTextInfo name) : base(name) { }

    public Character(string formattedName) : base(formattedName) { }

    #endregion

    #region Methods

    public Dictionary<Creation, CharacterRole> GetCreations() =>
        Creations.ToDictionary(cc => cc.Origin, cc => cc.Relation);

    public void AddCreations(Dictionary<Creation, CharacterRole> creations) =>
        creations.ToList().ForEach(AddCreation);
    
    public void AddCreation(KeyValuePair<Creation, CharacterRole> creation) =>
        AddCreation(creation.Key, creation.Value);

    public void AddCreation(Creation creation, CharacterRole role) =>
        Creations.Add(new(creation, this, role));

    #endregion
}
