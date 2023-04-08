namespace OpenHentai.Tags;

/// <inheritdoc />
public class Tag : ITag
{
    /// <inheritdoc />
    public string Value { get; set; }
    
    /// <inheritdoc />
    public IEnumerable<string> AlternativeValues { get; set; }
    
    /// <inheritdoc />
    public string Description { get; set; }
    
    /// <inheritdoc />
    public string AdditionalInfo { get; set; }
    
    /// <inheritdoc />
    public TagCategory Category { get; set; }
    
    public Tag(string value, TagCategory category)
    {
        Value = value;
        Category = category;
    }
}
