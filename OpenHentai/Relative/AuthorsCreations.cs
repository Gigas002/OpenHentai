using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.Roles;

namespace OpenHentai.Relative;

[Table("authors_creations")]
[PrimaryKey("author_id", "creation_id")]
public class AuthorsCreations
{
    #region Properties

    [ForeignKey("author_id")]
    public Author Author { get; set; } = null!;

    [ForeignKey("creation_id")]
    public Creation Creation { get; set; } = null!;

    public AuthorRole Role { get; set; }

    #endregion

    #region Constructors
    
    public AuthorsCreations() { }

    public AuthorsCreations(Author author, Creation creation, AuthorRole role) =>
        (Author, Creation, Role) = (author, creation, role);

    #endregion
}
