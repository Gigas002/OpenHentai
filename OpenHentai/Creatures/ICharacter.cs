using OpenHentai.Creations;
using OpenHentai.Relations;
using OpenHentai.Roles;

namespace OpenHentai.Creatures;

/// <summary>
/// Character
/// </summary>
public interface ICharacter : ICreature
{
    #region Properties
    
    /// <summary>
    /// Collection of creations, that has this character
    /// </summary>
    public IDictionary<ICreation, CharacterRole> InCreations { get; }

    #endregion
}
