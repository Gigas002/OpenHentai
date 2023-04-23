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
    public IEnumerable<LocalizableTextInfo> Text { get; set; }

    public DescriptionInfo(string descriptionLine)
    {
        Text = new[] { new LocalizableTextInfo(descriptionLine) };
    }

    public DescriptionInfo(IEnumerable<string> description)
    {
        Text = description.Select(line => new LocalizableTextInfo(line));
    }
}
