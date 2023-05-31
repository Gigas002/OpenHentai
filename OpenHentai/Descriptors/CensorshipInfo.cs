using System.Text.Json.Serialization;
using OpenHentai.Creations;

namespace OpenHentai.Descriptors;

/// <summary>
/// Information about censorship in creation
/// </summary>
public class CensorshipInfo
{
    /// <summary>
    /// Censorship type
    /// </summary>
    [JsonPropertyName("censorship")]
    public Censorship Censorship { get; set; }

    /// <summary>
    /// Is this kind of censorship official?
    /// </summary>
    [JsonPropertyName("is_official")]
    public bool IsOfficial { get; set; }

    public CensorshipInfo() { }
}
