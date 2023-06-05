using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OpenHentai.Descriptors;
using OpenHentai.Creatures;
using OpenHentai.Creations;
using OpenHentai.JsonConverters;
using OpenHentai.Circles;

namespace OpenHentai.Tags;

/*
 * Vision of tagging system (at that point of time)

      `tags.sql`

id category value description additional_info 


      `tags_relations.sql`

master_id slave_ids


     `tags_categories.sql` (not yet sure if we'll need it)

id value


Then, in `authors.sql`, e.g:


...            tag_ids             ...
...  [array of ids from `tags.sql`]  ...


the example of corresponding values in `tags.sql` will be:


id  cat_id    val
0     10    "genre1"
1     11    "clothes1"
2     12    "genre3"


in `tags_relations.sql`:


m_id    s_ids
 0      [1, 2]


in `tags_categories`:


id   value
10   genre
11  clothes

 * 
 */

// see: https://ehwiki.org/wiki/Gallery_Tagging for reference during the development
// also: https://repo.e-hentai.org/tools.php?act=taggroup&show=8 tag repo

/// <summary>
/// Basic interface for tagging purposes
/// </summary>

// [Index(nameof(Id), nameof(Value), IsUnique = true)]
public class Tag : IDatabaseEntity
{
    #region Properties

    /// <inheritdoc />
    public ulong Id { get; init; }
    
    /// <summary>
    /// This tag's master tag
    /// <para/>null in case there's no master
    /// </summary>
    [JsonConverter(typeof(DatabaseEntityJsonConverter<Tag>))]
    public Tag? Master { get; set; }
    
    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Tag>))]
    public HashSet<Tag> Slaves { get; init; } = new();

    /// <summary>
    /// Category of this tag
    /// e.g. `parody`
    /// </summary>
    [Required]
    public TagCategory Category { get; set; } = TagCategory.Unknown;
    
    /// <summary>
    /// Tag's value
    /// e.g. Mitsudomoe
    /// </summary>
    public string Value { get; set; } = null!;

    /// <summary>
    /// Tag description
    /// e.g. This tag resides for mitsudomoe franchise
    /// </summary>
    [Column(TypeName = "jsonb")]
    public HashSet<LanguageSpecificTextInfo> Description { get; init; } = new();

    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Creature>))]
    public HashSet<Creature> Creatures { get; init; } = new();

    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Creation>))]
    public HashSet<Creation> Creations { get; init; } = new();

    [JsonConverter(typeof(DatabaseEntityCollectionJsonConverter<Circle>))]
    public HashSet<Circle> Circles { get; init; } = new();

    #endregion
}
