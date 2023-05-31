using OpenHentai.Descriptors;
using OpenHentai.Database.Creatures;
using OpenHentai.Database.Creations;
using OpenHentai.Tags;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace OpenHentai.Database.Tags;

[Index(nameof(Id), nameof(Value), IsUnique = true)]
[Table("tags")]
public class Tag : ITag, IDatabaseEntity
{
    public ulong Id { get; set; }
    
    public ulong? MasterId { get; set; }
    
    [JsonIgnore]
    private Tag? Master { get; set; }
    
    public ITag? GetMaster() => Master;
    
    public void SetMaster(ITag tag) => Master = tag as Tag;
    
    [JsonIgnore]
    public IEnumerable<Tag> Slaves { get; set; } = null!;

    [Required]
    public TagCategory Category { get; set; } = TagCategory.Unknown;
    
    public string Value { get; set; } = null!;

    [Column(TypeName = "jsonb")]
    public IEnumerable<LanguageSpecificTextInfo>? Description { get; set; }

    [JsonIgnore]
    public IEnumerable<Creature>? Creatures { get; set; }

    [JsonIgnore]
    public IEnumerable<Creation>? Creations { get; set; }
}
