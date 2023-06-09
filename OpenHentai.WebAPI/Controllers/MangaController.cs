using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch.Operations;
using OpenHentai.Repositories;
using OpenHentai.Relative;
using OpenHentai.Circles;
using OpenHentai.Tags;
using OpenHentai.Roles;
using OpenHentai.Relations;
using OpenHentai.Descriptors;
using OpenHentai.WebAPI.Constants;
using OpenHentai.Creations;

namespace OpenHentai.WebAPI.Controllers;

/// <summary>
/// Controller, that works with Manga table and it's dependent ones
/// </summary>
// [AutoValidateAntiforgeryToken]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route(MangaRoutes.Base)]
public class MangaController : DatabaseController<IMangaRepository>, ICreationsController
{
    #region Constructors

    /// <inheritdoc/>
    public MangaController(IMangaRepository repository) : base(repository) { }

    #endregion

    #region Methods

    #region GET

    /// <summary>
    /// Get all manga
    /// </summary>
    /// <returns>Collection of Manga</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<Manga>> GetManga()
    {
        var manga = Repository.GetManga();

        return manga is null ? NotFound() : Ok(manga);
    }

    /// <summary>
    /// Get manga from database by id
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <returns>Manga</returns>
    [HttpGet(MangaRoutes.Id)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult<Manga>> GetMangaAsync(ulong id) => GetEntryAsync<Manga>(id);

    /// <summary>
    /// Get current manga's titles
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <returns>Collection of titles</returns>
    [HttpGet(MangaRoutes.Titles)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreationsTitles>>> GetTitlesAsync(ulong id)
    {
        var names = await Repository.GetTitlesAsync(id).ConfigureAwait(false);

        return names is null ? NotFound() : Ok(names);
    }

    /// <summary>
    /// Get current manga's authors
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <returns>Collection of AuthorsCreations</returns>
    [HttpGet(MangaRoutes.Authors)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<AuthorsCreations>>> GetAuthorsAsync(ulong id)
    {
        var authors = await Repository.GetAuthorsAsync(id).ConfigureAwait(false);

        return authors is null ? NotFound() : Ok(authors);
    }

    /// <summary>
    /// Get current manga's circles
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <returns>Collection of Circle</returns>
    [HttpGet(MangaRoutes.Circles)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Circle>>> GetCirclesAsync(ulong id)
    {
        var circles = await Repository.GetCirclesAsync(id).ConfigureAwait(false);

        return circles is null ? NotFound() : Ok(circles);
    }

    /// <summary>
    /// Get current manga's relations
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <returns>Collection of CreaturesRelations</returns>
    [HttpGet(MangaRoutes.Relations)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreaturesRelations>>> GetRelationsAsync(ulong id)
    {
        var relations = await Repository.GetRelationsAsync(id).ConfigureAwait(false);

        return relations is null ? NotFound() : Ok(relations);
    }

    /// <summary>
    /// Get current manga's characters
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <returns>Collection of CreationsCharacters</returns>
    [HttpGet(MangaRoutes.Characters)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreationsCharacters>>> GetCharactersAsync(ulong id)
    {
        var characters = await Repository.GetCharactersAsync(id).ConfigureAwait(false);

        return characters is null ? NotFound() : Ok(characters);
    }

    /// <summary>
    /// Get current manga's tags
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <returns>Collection of Tag</returns>
    [HttpGet(MangaRoutes.Tags)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTagsAsync(ulong id)
    {
        var tags = await Repository.GetTagsAsync(id).ConfigureAwait(false);

        return tags is null ? NotFound() : Ok(tags);
    }

    #endregion

    #region POST

    /// <summary>
    /// Add manga to database
    /// </summary>
    /// <param name="manga">Manga to add</param>
    /// <remarks>
    ///
    /// Minimal request:
    ///
    ///     POST /manga
    ///     { }
    ///
    /// </remarks>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Manga>> PostMangaAsync(Manga manga)
    {
        var isSuccess = await PostEntryAsync(manga).ConfigureAwait(false);

        return isSuccess ? CreatedAtAction(nameof(GetMangaAsync), new { id = manga.Id }, manga) : BadRequest();
    }

    /// <summary>
    /// Updates Manga with new titles, pushed at their own table
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="titles">Collection of new titles to push</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     POST /manga/{id}/titles
    ///     [{
    ///         "text": "taras panis",
    ///         "language": null
    ///     }]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPost(MangaRoutes.Titles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostTitlesAsync(ulong id, HashSet<LanguageSpecificTextInfo> titles)
    {
        var isSuccess = await Repository.AddTitlesAsync(id, titles).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Updates Manga with new relations to other creations
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="relations">Dictionary of related creation id and relation type</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     POST /manga/{id}/relations
    ///     {
    ///         "1": 1,
    ///         "2": 0
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPost(MangaRoutes.Relations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostRelationsAsync(ulong id, Dictionary<ulong, CreationRelations> relations)
    {
        var isSuccess = await Repository.AddRelationsAsync(id, relations).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region PUT

    /// <summary>
    /// Bind Manga to authors
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="authorRoles">Dictionary of author ids and AuthorRole to bind with this Manga</param>
    /// <remarks>
    ///
    /// Sample request:
    ///
    ///     PUT /manga/{id}/authors
    ///     {
    ///         "1": 3,
    ///         "4": 2
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(MangaRoutes.Authors)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutAuthorsAsync(ulong id, Dictionary<ulong, AuthorRole> authorRoles)
    {
        var isSuccess = await Repository.AddAuthorsAsync(id, authorRoles).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Bind Manga to collection of Circle
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="circleIds">Collection of Circle ids to bind with this Manga</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PUT /manga/{id}/circles
    ///     [
    ///         1, 2
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(MangaRoutes.Circles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        var isSuccess = await Repository.AddCirclesAsync(id, circleIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Bind Manga to characters
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="characterRoles">Dictionary of character ids and CharacterRole to bind with this Manga</param>
    /// <remarks>
    ///
    /// Sample request:
    ///
    ///     PUT /manga/{id}/characters
    ///     {
    ///         "1": 3,
    ///         "4": 2
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(MangaRoutes.Characters)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutCharactersAsync(ulong id, Dictionary<ulong, CharacterRole> characterRoles)
    {
        var isSuccess = await Repository.AddCharactersAsync(id, characterRoles).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Bind Manga to tags
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="tagIds">Collection of tag ids to bind with this Manga</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PUT /manga/{id}/tags
    ///     [
    ///         1, 2
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(MangaRoutes.Tags)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        var isSuccess = await Repository.AddTagsAsync(id, tagIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Delete Manga from database
    /// </summary>
    /// <param name="id">Id of Manga to delete</param>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(MangaRoutes.Id)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult> DeleteMangaAsync(ulong id) => DeleteEntryAsync<Manga>(id);

    /// <summary>
    /// Delete collection of titles, bound to Manga
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="titleIds">Collection of title ids to delete</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /manga/{id}/titles
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(MangaRoutes.Titles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteTitlesAsync(ulong id, HashSet<ulong> titleIds)
    {
        var isSuccess = await Repository.RemoveTitlesAsync(id, titleIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Manga and specified Authors
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="authorIds">Collection of Author ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /manga/{id}/authors
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(MangaRoutes.Authors)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteAuthorsAsync(ulong id, HashSet<ulong> authorIds)
    {
        var isSuccess = await Repository.RemoveAuthorsAsync(id, authorIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Manga and specified Circles
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="circleIds">Collection of Circle ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /manga/{id}/circles
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(MangaRoutes.Circles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        var isSuccess = await Repository.RemoveCirclesAsync(id, circleIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Manga and specified Creatures
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="relatedIds">Collection of Creature ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /manga/{id}/relations
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(MangaRoutes.Relations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteRelationsAsync(ulong id, HashSet<ulong> relatedIds)
    {
        var isSuccess = await Repository.RemoveRelationsAsync(id, relatedIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Manga and specified Characters
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="characterIds">Collection of Character ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /manga/{id}/characters
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(MangaRoutes.Characters)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteCharactersAsync(ulong id, HashSet<ulong> characterIds)
    {
        var isSuccess = await Repository.RemoveCharactersAsync(id, characterIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Manga and specified Tags
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="tagIds">Collection of Tag ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /manga/{id}/tags
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(MangaRoutes.Tags)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        var isSuccess = await Repository.RemoveTagsAsync(id, tagIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region PATCH

    /// <summary>
    /// Update Manga, using json-patch format
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <param name="operations">Collection of json-patch operations</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PATCH /manga/{id}
    ///     [{
    ///         "path": "/length",
    ///         "op": "replace",
    ///         "value": 30
    ///     }]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPatch(MangaRoutes.Id)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypes.JsonPatch)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult> PatchMangaAsync(ulong id, IEnumerable<Operation<Manga>> operations) =>
        PatchEntryAsync(id, operations);

    #endregion

    #endregion
}
