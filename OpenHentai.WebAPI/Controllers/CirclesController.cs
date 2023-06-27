using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch.Operations;
using OpenHentai.Creatures;
using OpenHentai.Repositories;
using OpenHentai.Relative;
using OpenHentai.Tags;
using OpenHentai.Descriptors;
using OpenHentai.WebAPI.Constants;
using OpenHentai.Circles;
using OpenHentai.Creations;

namespace OpenHentai.WebAPI.Controllers;

/// <summary>
/// Controller, that works with Circle table and it's dependent ones
/// </summary>
// [AutoValidateAntiforgeryToken]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route(CirclesRoutes.Base)]
public class CirclesController : DatabaseController<ICirclesRepository>
{
    #region Constructors

    /// <inheritdoc/>
    public CirclesController(ICirclesRepository repository) : base(repository) { }

    #endregion

    #region Methods

    #region GET

    /// <summary>
    /// Get all circles
    /// </summary>
    /// <returns>Collection of Circles</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<Circle>> GetCircles()
    {
        var circles = Repository.GetCircles();

        return circles is null ? NotFound() : Ok(circles);
    }

    /// <summary>
    /// Get circle from database by id
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <returns>Circle</returns>
    [HttpGet(CirclesRoutes.Id)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult<Circle>> GetCircleAsync(ulong id) => GetEntryAsync<Circle>(id);

    /// <summary>
    /// Get collection of all circles's titles
    /// </summary>
    /// <returns>Collection of CirclesTitles</returns>
    [HttpGet(CirclesRoutes.AllTitles)]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<CirclesTitles>> GetAllTitles()
    {
        var titles = Repository.GetAllTitles();

        return titles is null ? NotFound() : Ok(titles);
    }

    /// <summary>
    /// Get current circle's titles
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <returns>Collection of CirclesTitles</returns>
    [HttpGet(CirclesRoutes.Titles)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CirclesTitles>>> GetTitlesAsync(ulong id)
    {
        var titles = await Repository.GetTitlesAsync(id).ConfigureAwait(false);

        return titles is null ? NotFound() : Ok(titles);
    }

    /// <summary>
    /// Get current circle's authors
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <returns>Collection of Authors</returns>
    [HttpGet(CirclesRoutes.Authors)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsAsync(ulong id)
    {
        var authors = await Repository.GetAuthorsAsync(id).ConfigureAwait(false);

        return authors is null ? NotFound() : Ok(authors);
    }

    /// <summary>
    /// Get current circle's creations
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <returns>Collection of Creations</returns>
    [HttpGet(CirclesRoutes.Creations)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Creation>>> GetCreationsAsync(ulong id)
    {
        var creations = await Repository.GetCreationsAsync(id).ConfigureAwait(false);

        return creations is null ? NotFound() : Ok(creations);
    }

    /// <summary>
    /// Get current circle's tags
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <returns>Collection of Tag</returns>
    [HttpGet(CirclesRoutes.Tags)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTagsAsync(ulong id)
    {
        var tags = await Repository.GetTagsAsync(id).ConfigureAwait(false);

        return tags is null ? NotFound() : Ok(tags);
    }

    #endregion

    #region POST

    /// <summary>
    /// Add circle to database
    /// </summary>
    /// <param name="circle">Circle to add</param>
    /// <remarks>
    ///
    /// Minimal request:
    ///
    ///     POST /circles
    ///     { }
    ///
    /// </remarks>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Circle>> PostCircleAsync(Circle circle)
    {
        var isSuccess = await PostEntryAsync(circle).ConfigureAwait(false);

        return isSuccess ? CreatedAtAction(nameof(GetCircleAsync), new { id = circle.Id }, circle) : BadRequest();
    }

    /// <summary>
    /// Updates Circle with new titles, pushed at their own table
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <param name="titles">Collection of new titles to push</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     POST /circles/{id}/titles
    ///     [{
    ///         "text": "taras panis",
    ///         "language": null
    ///     }]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPost(CirclesRoutes.Titles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostTitlesAsync(ulong id, HashSet<LanguageSpecificTextInfo> titles)
    {
        var isSuccess = await Repository.AddTitlesAsync(id, titles).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region PUT

    /// <summary>
    /// Bind Circle to collection of Authors
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <param name="authorsIds">Collection of Authors ids to bind with this Circle</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PUT /circles/{id}/authors
    ///     [
    ///         1, 2
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(CirclesRoutes.Authors)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutAuthorsAsync(ulong id, HashSet<ulong> authorsIds)
    {
        var isSuccess = await Repository.AddAuthorsAsync(id, authorsIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Bind Circle to creations
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <param name="creationIds">Collection of creation ids to bind with this Circle</param>
    /// <remarks>
    ///
    /// Sample request:
    ///
    ///     PUT /circles/{id}/creations
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(CirclesRoutes.Creations)]
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
    /// Bind Circle to tags
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <param name="tagIds">Collection of tag ids to bind with this Circle</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PUT /circles/{id}/tags
    ///     [
    ///         1, 2
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(CirclesRoutes.Tags)]
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
    /// Delete Circle from database
    /// </summary>
    /// <param name="id">Id of Circle to delete</param>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(CirclesRoutes.Id)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult> DeleteCircleAsync(ulong id) => DeleteEntryAsync<Circle>(id);

    /// <summary>
    /// Delete collection of titles, bound to Circle
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <param name="titleIds">Collection of titles ids to delete</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /circles/{id}/titles
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(CirclesRoutes.Titles)]
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
    /// Delete binding between Circle and specified Authors
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <param name="authorIds">Collection of Author ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /circles/{id}/authors
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(CirclesRoutes.Authors)]
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
    /// Delete binding between Circle and specified Creations
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <param name="creationIds">Collection of Creation ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /circles/{id}/creations
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(CirclesRoutes.Creations)]
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
    /// Delete binding between Circle and specified Tags
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <param name="tagIds">Collection of Tag ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /circles/{id}/tags
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(CirclesRoutes.Tags)]
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
    /// Update Circle, using json-patch format
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <param name="operations">Collection of json-patch operations</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PATCH /circles/{id}
    ///     [{
    ///         "path": "/titles",
    ///         "op": "add",
    ///         "value": [{
    ///           "circle_id": 8,
    ///           "name": "Test Bubato",
    ///           "language": null
    ///         }]
    ///     }]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPatch(CirclesRoutes.Id)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypes.JsonPatch)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult> PatchCircleAsync(ulong id, IEnumerable<Operation<Circle>> operations) =>
        PatchEntryAsync(id, operations);

    #endregion

    #endregion
}
