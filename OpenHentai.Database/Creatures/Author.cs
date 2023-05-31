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
    
    public List<AuthorsNames> AuthorNames { get; set; } = new();

    /// <inheritdoc />
    public List<Circle> Circles { get; set; } = new();

    /// <inheritdoc />
    [Column(TypeName = "jsonb")]
    public List<ExternalLinkInfo> ExternalLinks { get; set; } = new();
    
    public List<AuthorsCreations> Creations { get; init; } = new();
    
    #endregion

    #endregion
}
