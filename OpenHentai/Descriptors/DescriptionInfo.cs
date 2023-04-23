namespace OpenHentai.Descriptors;

/// <summary>
/// Standard for json in non-indexed, localizable descriptions
/// </summary>
// TODO: WIP
public class DescriptionInfo
{
    /// <summary>
    /// Description's text
    /// </summary>
    public IEnumerable<LanguageSpecificTextInfo> Text { get; set; }

    public DescriptionInfo(string descriptionLine)
    {
        Text = new[] { new LanguageSpecificTextInfo(descriptionLine) };
    }

    public DescriptionInfo(IEnumerable<string> description)
    {
        Text = description.Select(line => new LanguageSpecificTextInfo(line));
    }
}
