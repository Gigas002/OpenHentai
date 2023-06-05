using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Constants;
using OpenHentai.Creations;
using OpenHentai.Creatures;
using OpenHentai.JsonConverters;
using OpenHentai.Roles;

namespace OpenHentai.Relative;

[PrimaryKey(FieldNames.AuthorId, FieldNames.CreationId)]
public class AuthorsCreations
{
    #region Properties

    [ForeignKey(FieldNames.AuthorId)]
    [JsonPropertyName(FieldNames.AuthorId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Author>))]
    public Author Author { get; set; } = null!;

    [ForeignKey(FieldNames.CreationId)]
    [JsonPropertyName(FieldNames.CreationId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Creation>))]
    public Creation Creation { get; set; } = null!;

    public AuthorRole Role { get; set; }

    #endregion

    #region Constructors

    public AuthorsCreations() { }

    public AuthorsCreations(Author author, Creation creation, AuthorRole role) =>
        (Author, Creation, Role) = (author, creation, role);

    #endregion
}
