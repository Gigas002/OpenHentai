using OpenHentai.Tags;
using OpenHentai.Descriptors;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenHentai.Database;

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
    // TODO: map this requires some work
    [NotMapped]
    public DescriptionInfo Description { get; set; }

    #endregion
}
