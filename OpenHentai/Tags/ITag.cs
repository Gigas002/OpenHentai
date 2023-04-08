namespace OpenHentai.Tags;

/// <summary>
/// Used for string-representated tags
/// </summary>
// see: https://ehwiki.org/wiki/Gallery_Tagging for reference during the development
// also: https://repo.e-hentai.org/tools.php?act=taggroup&show=8 tag repo
// TODO: master/slave tag system
public interface ITag
{
    /// <summary>
    /// Tag's value
    /// e.g. Mitsudomoe
    /// </summary>
    public string Value { get; set; }
    
    /// <summary>
    /// Alternative tag values
    /// e.g. Three Way Struggle, みつどもえ
    /// </summary>
    public IEnumerable<string> AlternativeValues { get; set; }
    
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
    /// Category of this tag
    /// </summary>
    public TagCategory Category { get; set; }
}
