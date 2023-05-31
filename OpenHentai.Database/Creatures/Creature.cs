using OpenHentai.Creatures;
using OpenHentai.Descriptors;
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
    
    public List<MediaInfo> Media { get; set; } = new();

    /// <inheritdoc />
    public Gender Gender { get ; set ; }
    
    /// <inheritdoc />
    public IEnumerable<Tag> Tags { get ; set ; }
    
    public List<CreaturesRelations> Relations { get; set; } = new();
}
