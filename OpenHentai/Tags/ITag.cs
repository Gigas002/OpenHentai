using OpenHentai.Descriptors;

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
public interface ITag
{
    #region Properties

    /// <summary>
    /// This tag's master tag
    /// <para/>null in case there's no master
    /// </summary>
    public ulong? MasterId { get; set; }
    
    /// <summary>
    /// Category of this tag
    /// e.g. `parody`
    /// </summary>
    public TagCategory Category { get; set; }
    
    /// <summary>
    /// Tag's value
    /// e.g. Mitsudomoe
    /// </summary>
    public string Value { get; set; }
    
    /// <summary>
    /// Tag description
    /// e.g. This tag resides for mitsudomoe franchise
    /// </summary>
    public DescriptionInfo Description { get; set; }

    #endregion

    #region Methods
    
    // Arguable if this is really needed inside interface
    
    // /// <summary>
    // /// Get tag from tag source
    // /// </summary>
    // /// <returns>ITag object</returns>
    // public ITag GetTag();
    //
    // /// <summary>
    // /// Get master tag from tag source
    // /// </summary>
    // /// <returns>ITag object</returns>
    // public ITag GetMaster();
    //
    // /// <summary>
    // /// Get slave tags in case it's master tag from tag source
    // /// </summary>
    // /// <returns>Collection of ITag objects</returns>
    // public IEnumerable<ITag> GetSlaves();

    #endregion
}
