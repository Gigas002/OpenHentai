using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Circles;
using OpenHentai.Database.Creations;
using OpenHentai.Descriptors;
using OpenHentai.Roles;

namespace OpenHentai.Database.Creatures;

/// <inheritdoc />
[Table("authors")]
public class Author : Creature //, IDatabaseEntity //, IAuthor
{
    #region Properties

    #region Interfaces implementation
    
    /// <inheritdoc />
    [NotMapped]
    public IEnumerable<LanguageSpecificTextInfo> AuthorNames { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public IEnumerable<Circle> Circles { get; set; }

    /// <inheritdoc />
    [NotMapped]
    public IEnumerable<ExternalLinkInfo> ExternalLinks { get; set; }
    
    /// <inheritdoc />
    [NotMapped]
    public IDictionary<Creation, AuthorRole> Creations { get; init; }
    
    #endregion

    #endregion
}
