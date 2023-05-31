using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Circles;
using OpenHentai.Database.Relative;
using OpenHentai.Descriptors;

namespace OpenHentai.Database.Creatures;

/// <inheritdoc />
[Table("authors")]
public class Author : Creature //, IDatabaseEntity //, IAuthor
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
    
    #endregion

    #endregion
}
