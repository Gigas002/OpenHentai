using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Roles;
using OpenHentai.Tags;
using OpenHentai.Creatures;
using OpenHentai.Database.Tags;

namespace OpenHentai.Database.Creatures;

/// <inheritdoc />
public class Author : IAuthor, IDatabaseEntity
{
    #region Properties

    #region Interfaces implementation

    public ulong Id { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public IEnumerable<LanguageSpecificTextInfo> Names { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public DescriptionInfo Description { get; set; }

    /// <inheritdoc />
    public DateTime Birthday { get; set; }
    
    /// <inheritdoc />
    public int Age { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public IEnumerable<PictureInfo> Pictures { get; set; }

    /// <inheritdoc />
    public Gender Gender { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public IEnumerable<ITag> Tags { get; set; } = new List<Tag>();

    /// <inheritdoc />
    [NotMapped]
    public IEnumerable<LanguageSpecificTextInfo> AuthorNames { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public IEnumerable<ICircle> Circles { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public IEnumerable<ExternalLinkInfo> ExternalLinks { get; set; }
    
    /// <inheritdoc />
    [NotMapped]
    public IDictionary<ICreation, AuthorRole> Creations { get; init; }
    
    /// <inheritdoc />
    public ulong CreatureId { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public IDictionary<ICreature, CreatureRelations> Relations { get; init; }

    #endregion

    #endregion
}
