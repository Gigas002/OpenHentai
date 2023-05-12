namespace OpenHentai.Database;

/// <summary>
/// Basic database entries specification
/// </summary>
public interface IDatabaseEntity
{
    /// <summary>
    /// Entry's id in database
    /// </summary>
    public ulong Id { get; set; }
}
