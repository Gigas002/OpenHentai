using OpenHentai.Creatures;
using OpenHentai.Descriptors;
using OpenHentai.Database.Tags;
using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Database.Relative;
using OpenHentai.Relations;
using OpenHentai.Tags;

namespace OpenHentai.Database.Creatures;

[Table("creatures")]
public class Creature : IDatabaseEntity, ICreature
{
    /// <inheritdoc />
    public ulong Id { get; set; }

    /// <inheritdoc />
    public IEnumerable<CreaturesNames> Names { get; set; } = null!;

    /// <inheritdoc />
    [Column(TypeName = "jsonb")]
    public IEnumerable<LanguageSpecificTextInfo>? Description { get ; set ; }
    
    /// <inheritdoc />
    public DateTime? Birthday { get ; set ; }
    
    /// <inheritdoc />
    public int Age { get ; set ; }
    
    public IEnumerable<MediaInfo>? Media { get; set; }

    /// <inheritdoc />
    public Gender Gender { get ; set ; }
    
    /// <inheritdoc />
    public IEnumerable<Tag> Tags { get ; set ; } = null!;
    
    public IEnumerable<CreaturesRelations>? Relations { get; set; }

    public IEnumerable<LanguageSpecificTextInfo> GetNames()
    {
        throw new NotImplementedException();
    }

    public Dictionary<ICreature, CreatureRelations> GetRelations()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ITag> GetTags() => Tags;
}
