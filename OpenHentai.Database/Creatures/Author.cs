using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Circles;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Relative;
using OpenHentai.Descriptors;
using OpenHentai.Roles;

namespace OpenHentai.Database.Creatures;

/// <inheritdoc />
[Table("authors")]
public class Author : Creature //, IDatabaseEntity //, IAuthor
{
    #region Properties

    #region Interfaces implementation
    
    public List<AuthorsNames> AuthorNames { get; set; } = new();

    /// <inheritdoc />
    public List<Circle> Circles { get; set; } = new();

    /// <inheritdoc />
    [NotMapped]
    public IEnumerable<ExternalLinkInfo> ExternalLinks { get; set; }
    
    /// <inheritdoc />
    [NotMapped]
    public IDictionary<Creation, AuthorRole> Creations { get; init; }
    
    #endregion

    #endregion
}
