namespace OpenHentai.Events;

/// <summary>
/// IRL event, e.g. comiket, comic1, etc
/// TODO: complete IEvent interface
/// </summary>
public interface IEvent : IDatabaseEntry
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; }
}
