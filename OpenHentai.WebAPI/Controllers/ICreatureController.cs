using Microsoft.AspNetCore.Mvc;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Relative;
using OpenHentai.Tags;

namespace OpenHentai.WebAPI.Controllers;

public interface ICreatureController
{
    #region GET

    public Task<ActionResult<IEnumerable<CreaturesNames>>> GetNamesAsync(ulong id);

    public Task<ActionResult<IEnumerable<Tag>>> GetTagsAsync(ulong id);

    public Task<ActionResult<IEnumerable<CreaturesRelations>>> GetRelationsAsync(ulong id);

    #endregion

    #region POST

    public Task<ActionResult> PostNamesAsync(ulong id, HashSet<LanguageSpecificTextInfo> names);

    public Task<ActionResult> PostRelationsAsync(ulong id, Dictionary<ulong, CreatureRelations> relations);

    #endregion

    #region PUT

    public Task<ActionResult> PutNamesAsync(ulong id, HashSet<ulong> nameIds);
    
    public Task<ActionResult> PutTagsAsync(ulong id, HashSet<ulong> tagIds);
    
    public Task<ActionResult> PutRelationsAsync(ulong id, Dictionary<ulong, CreatureRelations> relations);

    #endregion

    #region DELETE

    public Task<ActionResult> DeleteNamesAsync(ulong id, HashSet<ulong> nameIds);

    public Task<ActionResult> DeleteTagsAsync(ulong id, HashSet<ulong> tagIds);

    public Task<ActionResult> DeleteRelationsAsync(ulong id, HashSet<ulong> relatedIds);

    #endregion
}
