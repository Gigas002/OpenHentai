namespace OpenHentai;

/// <summary>
/// Database entries specification
/// </summary>
public interface IDatabaseEntry
{
    /// <summary>
    /// Entry's id in database
    /// </summary>
    public ulong Id { get; set; }
}
