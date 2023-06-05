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
public class AuthorsCreations : IRelativeDatabaseEntity<Author, Creation, AuthorRole>
{
    #region Properties

    [ForeignKey(FieldNames.AuthorId)]
    [JsonPropertyName(FieldNames.AuthorId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Author>))]
    public Author Origin { get; set; } = null!;

    [ForeignKey(FieldNames.CreationId)]
    [JsonPropertyName(FieldNames.CreationId)]
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Creation>))]
    public Creation Related { get; set; } = null!;

    public AuthorRole Relation { get; set; }

    #endregion

    #region Constructors

    public AuthorsCreations() { }

    public AuthorsCreations(Author author, Creation creation, AuthorRole role) =>
        (Origin, Related, Relation) = (author, creation, role);

    #endregion
}
