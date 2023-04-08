namespace OpenHentai.Tags;

/// <summary>
/// Used for fetishes and genres. Genders, languages, authors, etc has it's own classes
/// see https://ehwiki.org/wiki/Gallery_Tagging for reference during the development
/// </summary>
[Obsolete("Use ITag2")]
public interface ITagObsolete
{
    /// <summary>
    /// Tag description
    /// e.g. This tag resides for mitsudomoe franchise
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Additional info, related to tag
    /// e.g. link to mal: https://myanimelist.net/anime/7627
    /// </summary>
    public string AdditionalInfo { get; set; }

    /// <summary>
    /// Set tag's value from it's string representation
    /// </summary>
    /// <param name="value">Tag's string representation</param>
    public void SetValue(string value);
    
    /// <summary>
    /// Get string representation of tag's value
    /// </summary>
    /// <returns>String representation of tag's value</returns>
    public string GetValue();

    /// <summary>
    /// Set alternative tag values from it's string representations
    /// </summary>
    /// <param name="alternativeValues">Alternative tag values's string representation</param>
    public void SetAlternativeValues(IEnumerable<string> alternativeValues);
    
    /// <summary>
    /// Get string represetntation of tag's alternative values
    /// </summary>
    /// <returns>String represetntation of tag's alternative values</returns>
    public IEnumerable<string> GetAlternativeValues();

    /// <summary>
    /// Category of this tag
    /// </summary>
    public TagCategory Category { get; set; }
}

/// <inheritdoc />
/// <typeparam name="T">Must be correctly convertable to/from string</typeparam>
[Obsolete("Use props to describe something")]
public interface ITag<T> : ITagObsolete
{
    /// <summary>
    /// Tag's value
    /// e.g. Mitsudomoe
    /// </summary>
    public T Value { get; set; }
    
    /// <summary>
    /// Alternative tag values
    /// e.g. Three Way Struggle, みつどもえ
    /// </summary>
    public IEnumerable<T> AlternativeValues { get; set; }
}
