using Microsoft.AspNetCore.Mvc;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Relative;
using OpenHentai.Tags;

public interface ICreatureController
{
    #region GET

    public Task<ActionResult<IEnumerable<CreaturesNames>>> GetNamesAsync(ulong id);

    public Task<ActionResult<IEnumerable<Tag>>> GetTagsAsync(ulong id);

    public Task<ActionResult<IEnumerable<CreaturesRelations>>> GetRelationsAsync(ulong id);

    #endregion

    #region POST

    public Task<ActionResult> PostNamesAsync(ulong id, IEnumerable<LanguageSpecificTextInfo> names);

    public Task<ActionResult> PostTagsAsync(ulong id, IEnumerable<ulong> tagIds);

    public Task<ActionResult> PostRelationsAsync(ulong id, Dictionary<ulong, CreatureRelations> relations);

    #endregion

    #region PUT

    public Task<ActionResult> PutNamesAsync(ulong id, IEnumerable<ulong> nameIds);
    
    public Task<ActionResult> PutTagsAsync(ulong id, IEnumerable<ulong> tagIds);
    
    public Task<ActionResult> PutRelationsAsync(ulong id, Dictionary<ulong, CreatureRelations> relations);

    #endregion

    #region DELETE



    #endregion
}
