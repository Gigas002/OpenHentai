using OpenHentai.Tags;
using OpenHentai.Descriptors;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenHentai.Database.Tags;

/// <inheritdoc />
public class Tag : ITag, IDatabaseEntity
{
    #region Properties

    /// <inheritdoc />
    public ulong Id { get; set; }

    /// <inheritdoc />
    public ulong? MasterId { get; set; }

    public Tag Master { get; set; } = null!;

    public IEnumerable<Tag> Slaves { get; set; }
    
    /// <inheritdoc />
    public TagCategory Category { get; set; }
    
    /// <inheritdoc />
    public string Value { get; set; }
    
    /// <inheritdoc />
    [Column(TypeName = "jsonb")]
    public DescriptionInfo Description { get; set; }

    public ITag? GetMaster()
    {
        throw new NotImplementedException();
    }

    #endregion
}
