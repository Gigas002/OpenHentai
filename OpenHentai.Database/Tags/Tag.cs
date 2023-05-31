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
    public Tag? Master { get; set; }
    
    public ITag? GetMaster() => Master;
    
    public void SetMaster(ITag tag) => Master = tag as Tag;
    
    [JsonIgnore]
    public IEnumerable<Tag> Slaves { get; set; } = null!;

    [Required]
    public TagCategory Category { get; set; } = TagCategory.Unknown;
    
    public string Value { get; set; } = null!;

    [Column(TypeName = "jsonb")]
    public DescriptionInfo? Description { get; set; }

    [JsonIgnore]
    public List<Creature> Creatures { get; set; } = new();

    [JsonIgnore]
    public List<Creation> Creations { get; set; } = new();
}
