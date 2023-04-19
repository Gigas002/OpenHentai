using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Tags;

namespace OpenHentai.Creatures;

/// <summary>
/// Creature
/// </summary>
public interface ICreature : IDatabaseEntry
{
    /// <summary>
    /// Main name must be romanization of native name (e.g. Hepburn romanization for ja-JP)
    /// Alternative names can be any
    /// </summary>
    public IEnumerable<string> Names { get; set; }

    /// <summary>
    /// Additional details, e.g. hobby:..., three sizes:...
    /// </summary>
    public IEnumerable<string> AdditionalDetails { get; set; }

    /// <summary>
    /// Description, e.g. this person is a dick
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Creature's birthday, e.g. 01.01.1922
    /// </summary>
    public DateTime Birthday { get; set; }
    
    /// <summary>
    /// Creature's age, e.g. 500
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Collection of related pictures
    /// </summary>
    public IEnumerable<PictureInfo> Pictures { get; set; }

    /// <summary>
    /// Creature's gender
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Creature's additional details/tags
    /// </summary>
    public IEnumerable<ITag> Tags { get; set; }

    /// <summary>
    /// Creature ID
    /// </summary>
    public ulong CreatureId { get; set; }
    
    /// <summary>
    /// Collection of related and alternative characters,
    /// Chara-Relation pair, e.g. "Admiral, alternative"
    /// </summary>
    public IDictionary<ICreature, CreatureRelations> Relations { get; }
}
