using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Database.Circles;
using OpenHentai.Database.Relative;
using OpenHentai.Descriptors;
using OpenHentai.Roles;

namespace OpenHentai.Database.Creatures;

/// <inheritdoc />
[Table("authors")]
public class Author : Creature, IAuthor
{
    #region Properties

    #region Interfaces implementation
    
    public IEnumerable<AuthorsNames> AuthorNames { get; set; } = null!;

    /// <inheritdoc />
    public IEnumerable<Circle>? Circles { get; set; }

    /// <inheritdoc />
    [Column(TypeName = "jsonb")]
    public IEnumerable<ExternalLinkInfo>? ExternalLinks { get; set; }
    
    public IEnumerable<AuthorsCreations>? Creations { get; set; }

    public IEnumerable<LanguageSpecificTextInfo> GetAuthorNames()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ICircle> GetCircles() => Circles;

    public Dictionary<ICreation, AuthorRole> GetCreations()
    {
        throw new NotImplementedException();
    }

    #endregion

    #endregion
}
