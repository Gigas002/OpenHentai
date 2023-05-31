using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Creatures;
using OpenHentai.Roles;

namespace OpenHentai.Database.Relative;

[Table("authors_creations")]
[PrimaryKey("author_id", "creation_id")]
public class AuthorsCreations
{
    [ForeignKey("author_id")]
    public Author Author { get; set; } = null!;

    [ForeignKey("creation_id")]
    public Creation Creation { get; set; } = null!;

    public AuthorRole Role { get; set; }
}
