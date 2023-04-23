using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using OpenHentai.Tags;

namespace OpenHentai.Creatures;

/// <inheritdoc />
public class Author : IAuthor
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
    public IEnumerable<string> AuthorNames { get; set; }

    /// <inheritdoc />
    public IEnumerable<ICircle> Circles { get; set; }

    /// <inheritdoc />
    public IEnumerable<ExternalLinkInfo> ExternalLinks { get; set; }
    
    /// <inheritdoc />
    public IDictionary<ICreation, AuthorRole> Creations { get; init; }

    /// <inheritdoc />
    public ulong Id { get; set; }
    
    /// <inheritdoc />
    public ulong CreatureId { get; set; }

    /// <inheritdoc />
    public IDictionary<ICreature, CreatureRelations> Relations { get; init; }

    #endregion

    #endregion
}
