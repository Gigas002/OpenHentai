namespace OpenHentai.Tags;

/*
 * Used for:
 * Franchise (one creation/creature can have multiple franchises in tags)
 * Species
 * Theme
 * Any other non-enum/custom class tag
 */

/// <summary>
/// Generic class for string tags
/// </summary>
[Obsolete("Use props to describe something")]
public class StringTag : ITag<string>
{
    #region Properties

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
    
    #endregion

    #region Methods

    /// <inheritdoc />
    public void SetValue(string value) => Value = value;

    /// <inheritdoc />
    public string GetValue() => Value;

    /// <inheritdoc />
    public void SetAlternativeValues(IEnumerable<string> alternativeValues) => AlternativeValues = alternativeValues;

    /// <inheritdoc />
    public IEnumerable<string> GetAlternativeValues() => AlternativeValues;

    #endregion

    public StringTag(string value, TagCategory category)
    {
        Value = value;
        Category = category;
    }
}
