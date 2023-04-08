namespace OpenHentai.Tags;

/*
 * Used for:
 * AgeRating, Censorship, Genre, PublishStatus
 * Gender, BodyType
 * Genitals
 */

/// <summary>
/// Generic class for enum tags
/// </summary>
/// <typeparam name="T">Enum</typeparam>
[Obsolete("Use props to describe something")]
public class EnumTag<T> : ITag<T> where T : struct, Enum
{
    #region Properties

    /// <inheritdoc />
    public T Value { get; set; }
    
    /// <inheritdoc />
    public IEnumerable<T> AlternativeValues { get; set; }
  
    /// <inheritdoc />
    public string Description { get; set; }
    
    /// <inheritdoc />
    public string AdditionalInfo { get; set; }

    /// <inheritdoc />
    public TagCategory Category { get; set; }
    
    #endregion

    #region Methods
    
    /// <inheritdoc />
    public void SetValue(string value) => Value = Enum.Parse<T>(value);
    
    /// <inheritdoc />
    public string GetValue() => Value.ToString();
    
    /// <inheritdoc />
    public void SetAlternativeValues(IEnumerable<string> alternativeValues) => AlternativeValues = alternativeValues.Select(Enum.Parse<T>);

    /// <inheritdoc />
    public IEnumerable<string> GetAlternativeValues() => AlternativeValues.Select(value => value.ToString());
    
    #endregion

    public EnumTag(T value)
    {
        Value = value;
        Category = Enum.Parse<TagCategory>(nameof(T));
    }
}
