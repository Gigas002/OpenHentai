using OpenHentai.Descriptors;
using OpenHentai.Creatures;
using OpenHentai.Creations;
using OpenHentai.Tags;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
// using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using OpenHentai.JsonConverters;

namespace OpenHentai.Tags;

// [Index(nameof(Id), nameof(Value), IsUnique = true)]
[Table("tags")]
public class Tag : IDatabaseEntity//, ITag
{
    #region Properties

    public ulong Id { get; set; }
    
    [JsonConverter(typeof(TagMasterJsonConverter))]
    public Tag? Master { get; set; }
    
    [JsonIgnore]
    public HashSet<Tag> Slaves { get; init; } = new();

    [Required]
    public TagCategory Category { get; set; } = TagCategory.Unknown;
    
    public string Value { get; set; } = null!;

    [Column(TypeName = "jsonb")]
    public HashSet<LanguageSpecificTextInfo> Description { get; init; } = new();

    [JsonIgnore]
    public HashSet<Creature> Creatures { get; init; } = new();

    [JsonIgnore]
    public HashSet<Creation> Creations { get; init; } = new();

    #endregion
}
