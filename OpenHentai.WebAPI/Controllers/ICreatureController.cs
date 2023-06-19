using Microsoft.AspNetCore.Mvc;
using OpenHentai.Relative;

public interface ICreatureController
{
    public abstract ActionResult<IEnumerable<CreaturesNames>> GetCreatureNames(ulong id);
}
