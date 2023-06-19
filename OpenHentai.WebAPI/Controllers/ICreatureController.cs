using Microsoft.AspNetCore.Mvc;
using OpenHentai.Relative;
using OpenHentai.Tags;

public interface ICreatureController
{
    public abstract Task<ActionResult<IEnumerable<CreaturesNames>>> GetCreatureNamesAsync(ulong id);

    public abstract Task<ActionResult<IEnumerable<Tag>>> GetCreatureTagsAsync(ulong id);

    public abstract Task<ActionResult<IEnumerable<CreaturesRelations>>> GetCreatureRelationsAsync(ulong id);
}
