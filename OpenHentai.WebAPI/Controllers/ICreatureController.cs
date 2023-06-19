using Microsoft.AspNetCore.Mvc;
using OpenHentai.Relative;
using OpenHentai.Tags;

public interface ICreatureController
{
    public abstract ActionResult<IEnumerable<CreaturesNames>> GetCreatureNames(ulong id);

    public abstract ActionResult<IEnumerable<Tag>> GetCreatureTags(ulong id);

    public abstract ActionResult<IEnumerable<CreaturesRelations>> GetCreatureRelations(ulong id);
}
