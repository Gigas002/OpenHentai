using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Database.Tags;
using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;

namespace OpenHentai.Database.Creatures;

[Table("creatures")]
public class Creature : IDatabaseEntity //: ICreature
{
    /// <inheritdoc />
    public ulong Id { get; set; }

    /// <inheritdoc />
    public List<CreaturesNames> Names { get; init; } = new();    

    /// <inheritdoc />
    [Column(TypeName = "jsonb")]
    public DescriptionInfo? Description { get ; set ; }
    
    /// <inheritdoc />
    public DateTime Birthday { get ; set ; }
    
    /// <inheritdoc />
    public int Age { get ; set ; }
    
    /// <inheritdoc />
    [NotMapped]
    public IEnumerable<PictureInfo> Pictures { get ; set ; }
    
    /// <inheritdoc />
    public Gender Gender { get ; set ; }
    
    /// <inheritdoc />
    public IEnumerable<Tag> Tags { get ; set ; }
    
    /// <inheritdoc />
    [NotMapped]
    public IDictionary<ICreature, CreatureRelations> Relations { get; set; }
}
