using Microsoft.AspNetCore.Mvc;
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

    public Task<ActionResult> PostNamesAsync(ulong id, IEnumerable<CreaturesNames> names);

    public Task<ActionResult> PostTagsAsync(ulong id, IEnumerable<ulong> tagIds);

    #endregion
}
