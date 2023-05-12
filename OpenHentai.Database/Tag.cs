using OpenHentai.Tags;
using OpenHentai.Descriptors;

namespace OpenHentai.Database;

/// <inheritdoc />
public class Tag : ITag
{
    #region Properties

    /// <inheritdoc />
    public ulong? MasterId { get; set; }
    
    /// <inheritdoc />
    public IEnumerable<ulong> SlaveIds { get; set; }
    
    /// <inheritdoc />
    public TagCategory Category { get; set; }
    
    /// <inheritdoc />
    public string Value { get; set; }
    
    /// <inheritdoc />
    public DescriptionInfo Description { get; set; }

    #endregion
}
