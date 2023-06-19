using System.ComponentModel.DataAnnotations.Schema;
using OpenHentai.Creatures;
using OpenHentai.Creations;
using OpenHentai.Relative;
using OpenHentai.Descriptors;
using System.Text.Json.Serialization;
using OpenHentai.JsonConverters;
using OpenHentai.Tags;
using OpenHentai.Constants;

namespace OpenHentai.Circles;

/// <summary>
/// Author's circle
/// </summary>
[Table(TableNames.Circles)]
public class Circle : IDatabaseEntity
{
    #region Properties

    /// <inheritdoc />
    public ulong Id { get; set; }

    /// <summary>
    /// Main title must be romanization of native title (e.g. Hepburn romanization for ja-JP)
    /// Alternative titles can be any
    /// e.g. "ja-JP:ポプテピピック;en-US:Pop team epic"
    /// </summary>
    [JsonIgnore]
    public HashSet<CirclesTitles> CirclesTitles { get; init; } = new();

    /// <summary>
    /// Related authors
    /// </summary>
    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Author>))]
    public HashSet<Author> Authors { get; init; } = new();

    /// <summary>
    /// Related creations
    /// </summary>
    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Creation>))]
    public HashSet<Creation> Creations { get; init; } = new();

    /// <summary>
    /// Tags
    /// </summary>
    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Tag>))]
    public HashSet<Tag> Tags { get; init; } = new();

    #endregion

    #region Constructors

    /// <summary>
    /// Create a new circle
    /// </summary>
    public Circle() { }

    /// <inheritdoc cref="Circle()" />
    /// <param name="id">Id</param>
    public Circle(ulong id) => Id = id;

    /// <inheritdoc cref="Circle()" />
    /// <param name="title">Title</param>
    public Circle(LanguageSpecificTextInfo title) => AddTitle(title);
    
    public Circle(string formattedTitle) : this(new LanguageSpecificTextInfo(formattedTitle)) { }

    #endregion

    #region Methods

    /// <summary>
    /// Convert relational database's object into collection of formatted objects
    /// </summary>    
    public IEnumerable<LanguageSpecificTextInfo> GetTitles() =>
        CirclesTitles.Select(t => t.GetLanguageSpecificTextInfo());

    /// <summary>
    /// Add titles to the relational database
    /// </summary>   
    /// <param name="titles">Titles</param>
    public void AddTitles(IEnumerable<LanguageSpecificTextInfo> titles) =>
        titles.ToList().ForEach(AddTitle);

    /// <summary>
    /// Add title to the relational database
    /// </summary>
    /// <param name="title">Title</param>
    public void AddTitle(LanguageSpecificTextInfo title) => CirclesTitles.Add(new(this, title));

    public void AddTitle(string formattedTitle) =>
        AddTitle(new LanguageSpecificTextInfo(formattedTitle));

    #endregion
}
