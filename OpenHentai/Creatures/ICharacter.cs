using OpenHentai.Creations;
using OpenHentai.Relations;

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
    public IDictionary<ICreation, CharacterCreationRelation> FeaturedIn { get; }

    // /// <summary>
    // /// Character's creators
    // /// </summary>
    // public IEnumerable<IAuthor> Authors { get; set; }
    
    #endregion
}
