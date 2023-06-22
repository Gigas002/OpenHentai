using Microsoft.AspNetCore.Mvc;
using OpenHentai.Circles;
using OpenHentai.Descriptors;
using OpenHentai.Relations;
using OpenHentai.Relative;
using OpenHentai.Roles;
using OpenHentai.Tags;

namespace OpenHentai.WebAPI.Controllers;

public interface ICreationsController
{
    #region GET

    public Task<ActionResult<IEnumerable<CreationsTitles>>> GetTitlesAsync(ulong id);

    public Task<ActionResult<IEnumerable<AuthorsCreations>>> GetAuthorsAsync(ulong id);

    public Task<ActionResult<IEnumerable<Circle>>> GetCirclesAsync(ulong id);
    
    public Task<ActionResult<IEnumerable<CreaturesRelations>>> GetRelationsAsync(ulong id);

    public Task<ActionResult<IEnumerable<CreationsCharacters>>> GetCharactersAsync(ulong id);

    public Task<ActionResult<IEnumerable<Tag>>> GetTagsAsync(ulong id);

    #endregion

    #region POST

    public Task<ActionResult> PostTitlesAsync(ulong id, HashSet<LanguageSpecificTextInfo> titles);

    public Task<ActionResult> PostRelationsAsync(ulong id, Dictionary<ulong, CreationRelations> relations);

    #endregion

    #region PUT

    public Task<ActionResult> PutAuthorsAsync(ulong id, Dictionary<ulong, AuthorRole> authorRoles);

    public Task<ActionResult> PutCirclesAsync(ulong id, HashSet<ulong> circleIds);

    public Task<ActionResult> PutCharactersAsync(ulong id, Dictionary<ulong, CharacterRole> characterRoles);

    public Task<ActionResult> PutTagsAsync(ulong id, HashSet<ulong> tagIds);

    #endregion

    #region DELETE

    public Task<ActionResult> DeleteTitlesAsync(ulong id, HashSet<ulong> titleIds);

    public Task<ActionResult> DeleteAuthorsAsync(ulong id, HashSet<ulong> authorIds);

    public Task<ActionResult> DeleteCirclesAsync(ulong id, HashSet<ulong> circleIds);

    public Task<ActionResult> DeleteRelationsAsync(ulong id, HashSet<ulong> relatedIds);

    public Task<ActionResult> DeleteCharactersAsync(ulong id, HashSet<ulong> characterIds);

    public Task<ActionResult> DeleteTagsAsync(ulong id, HashSet<ulong> tagIds);

    #endregion
}
