using Microsoft.AspNetCore.Mvc;
using OpenHentai.Relative;
using OpenHentai.Tags;

public interface ICreatureController
{

    #region GET

    public Task<ActionResult<IEnumerable<CreaturesNames>>> GetCreatureNamesAsync(ulong id);

    public Task<ActionResult<IEnumerable<Tag>>> GetCreatureTagsAsync(ulong id);

    public Task<ActionResult<IEnumerable<CreaturesRelations>>> GetCreatureRelationsAsync(ulong id);

    #endregion

    #region POST

    public Task<ActionResult> PostCreatureNamesAsync(ulong id, IEnumerable<CreaturesNames> names);

    public Task<ActionResult> PostCreatureTagsAsync(ulong id, IEnumerable<ulong> tagIds);

    #endregion
}
