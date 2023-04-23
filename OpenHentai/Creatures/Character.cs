using OpenHentai.Creations;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using OpenHentai.Tags;

namespace OpenHentai.Creatures;

/// <inheritdoc />
public class Character : ICharacter
{
    #region Properties

    #region Interfaces implementation

    /// <inheritdoc />
    public IEnumerable<string> Names { get; set; }

    /// <inheritdoc />
    public string Description { get; set; }

    /// <inheritdoc />
    public DateTime Birthday { get; set; }
    
    /// <inheritdoc />
    public int Age { get; set; }

    /// <inheritdoc />
    public IEnumerable<PictureInfo> Pictures { get; set; }

    /// <inheritdoc />
    public Gender Gender { get; set; }
    
    /// <inheritdoc />
    public IEnumerable<ITag> Tags { get; set; }

    /// <inheritdoc />
    public IDictionary<ICreature, CreatureRelations> Relations { get; init; }

    /// <inheritdoc />
    public IDictionary<ICreation, CharacterRole> InCreations { get; init; }
    
    /// <inheritdoc />
    public ulong Id { get; set; }
    
    /// <inheritdoc />
    public ulong CreatureId { get; set; }
    
    #endregion

    #endregion
}
