using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch.Operations;
using OpenHentai.Repositories;
using OpenHentai.Circles;
using OpenHentai.Tags;
using OpenHentai.WebAPI.Constants;
using OpenHentai.Creations;

namespace OpenHentai.WebAPI.Controllers;

/// <summary>
/// Controller, that works with Tag table and it's dependent ones
/// </summary>
// [AutoValidateAntiforgeryToken]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route(TagsRoutes.Base)]
public class TagsController : DatabaseController<ITagsRepository>
{
    #region Constructors

    /// <inheritdoc/>
    public TagsController(ITagsRepository repository) : base(repository) { }

    #endregion

    #region Methods

    #region GET

    /// <summary>
    /// Get all tags
    /// </summary>
    /// <returns>Collection of Tags</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<Tag>> GetTags()
    {
        var tags = Repository.GetTags();

        return tags is null ? NotFound() : Ok(tags);
    }

    /// <summary>
    /// Get tag from database by id
    /// </summary>
    /// <param name="id">Tag's id</param>
    /// <returns>Tag</returns>
    [HttpGet(TagsRoutes.Id)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult<Tag>> GetTagAsync(ulong id) => GetEntryAsync<Tag>(id);

    /// <summary>
    /// Get current tag's creatures
    /// </summary>
    /// <param name="id">Tag's id</param>
    /// <returns>Collection of Creature</returns>
    [HttpGet(TagsRoutes.Creatures)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Circle>>> GetCreaturesAsync(ulong id)
    {
        var creatures = await Repository.GetCreaturesAsync(id).ConfigureAwait(false);

        return creatures is null ? NotFound() : Ok(creatures);
    }

    /// <summary>
    /// Get current tag's creations
    /// </summary>
    /// <param name="id">Tag's id</param>
    /// <returns>Collection of Creation</returns>
    [HttpGet(TagsRoutes.Creations)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Creation>>> GetCreationsAsync(ulong id)
    {
        var creations = await Repository.GetCreationsAsync(id).ConfigureAwait(false);

        return creations is null ? NotFound() : Ok(creations);
    }

    /// <summary>
    /// Get current tag's circles
    /// </summary>
    /// <param name="id">Tag's id</param>
    /// <returns>Collection of Circle</returns>
    [HttpGet(TagsRoutes.Circles)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Circle>>> GetCirclesAsync(ulong id)
    {
        var circles = await Repository.GetCirclesAsync(id).ConfigureAwait(false);

        return circles is null ? NotFound() : Ok(circles);
    }

    #endregion

    #region POST

    /// <summary>
    /// Add tag to database
    /// </summary>
    /// <param name="tag">Tag to add</param>
    /// <remarks>
    ///
    /// Minimal request:
    ///
    ///     POST /tags
    ///     { }
    ///
    /// </remarks>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Tag>> PostTagAsync(Tag tag)
    {
        var isSuccess = await PostEntryAsync(tag).ConfigureAwait(false);

        return isSuccess ? CreatedAtAction(nameof(GetTagAsync), new { id = tag.Id }, tag) : BadRequest();
    }

    #endregion

    #region PUT

    /// <summary>
    /// Bind Tag to collection of Creature
    /// </summary>
    /// <param name="id">Tag's id</param>
    /// <param name="creatureIds">Collection of creature ids to bind with this Tag</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PUT /tags/{id}/creatures
    ///     [
    ///         1, 2
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(TagsRoutes.Creatures)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutCreaturesAsync(ulong id, HashSet<ulong> creatureIds)
    {
        var isSuccess = await Repository.AddCreaturesAsync(id, creatureIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Bind Tag to creations
    /// </summary>
    /// <param name="id">Tag's id</param>
    /// <param name="creationIds">Collection of creation ids to bind with this Tag</param>
    /// <remarks>
    ///
    /// Sample request:
    ///
    ///     PUT /tags/{id}/creations
    ///     [
    ///         1, 2
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(TagsRoutes.Creations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutCreationsAsync(ulong id, HashSet<ulong> creationIds)
    {
        var isSuccess = await Repository.AddCreationsAsync(id, creationIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Bind Tag to collection of Circle
    /// </summary>
    /// <param name="id">Tag's id</param>
    /// <param name="circleIds">Collection of Circle ids to bind with this Tag</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PUT /tags/{id}/circles
    ///     [
    ///         1, 2
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(TagsRoutes.Circles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        var isSuccess = await Repository.AddCirclesAsync(id, circleIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Delete Tag from database
    /// </summary>
    /// <param name="id">Id of Tag to delete</param>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(TagsRoutes.Id)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult> DeleteTagAsync(ulong id) => DeleteEntryAsync<Tag>(id);

    /// <summary>
    /// Delete binding between Tag and specified Creatures
    /// </summary>
    /// <param name="id">Tag's id</param>
    /// <param name="creatureIds">Collection of Creature ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /tags/{id}/creatures
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(TagsRoutes.Creatures)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteCreaturesAsync(ulong id, HashSet<ulong> creatureIds)
    {
        var isSuccess = await Repository.RemoveCreaturesAsync(id, creatureIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Tag and specified Creations
    /// </summary>
    /// <param name="id">Tag's id</param>
    /// <param name="creationIds">Collection of Creation ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /tags/{id}/creations
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(TagsRoutes.Creations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteCreationsAsync(ulong id, HashSet<ulong> creationIds)
    {
        var isSuccess = await Repository.RemoveCreationsAsync(id, creationIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Tag and specified Circles
    /// </summary>
    /// <param name="id">Tag's id</param>
    /// <param name="circleIds">Collection of Circle ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /tags/{id}/circles
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(TagsRoutes.Circles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        var isSuccess = await Repository.RemoveCirclesAsync(id, circleIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region PATCH

    /// <summary>
    /// Update Tag, using json-patch format
    /// </summary>
    /// <param name="id">Tag's id</param>
    /// <param name="operations">Collection of json-patch operations</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PATCH /tags/{id}
    ///     [{
    ///         "path": "/value",
    ///         "op": "replace",
    ///         "value": "karasique"
    ///     }]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPatch(TagsRoutes.Id)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypes.JsonPatch)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult> PatchTagAsync(ulong id, IEnumerable<Operation<Tag>> operations) =>
        PatchEntryAsync(id, operations);

    #endregion

    #endregion
}
