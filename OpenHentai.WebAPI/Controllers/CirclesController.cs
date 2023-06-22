using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch.Operations;
using OpenHentai.Creatures;
using OpenHentai.Contexts;
using OpenHentai.Relative;
using OpenHentai.Tags;
using OpenHentai.Descriptors;
using OpenHentai.WebAPI.Constants;
using OpenHentai.Circles;
using OpenHentai.Creations;

namespace OpenHentai.WebAPI.Controllers;

#pragma warning disable CA1303

/// <summary>
/// Controller, that works with Circle table and it's dependent ones
/// </summary>
// [AutoValidateAntiforgeryToken]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route(CirclesRoutes.Base)]
public class CirclesController : DatabaseController<CirclesContextHelper>
{
    #region Constructors

    /// <inheritdoc/>
    public CirclesController(CirclesContextHelper contextHelper) : base(contextHelper) { }

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
        Console.WriteLine($"Enter into GET: /circles");

        var circles = ContextHelper.GetCircles();

        return circles is null ? NotFound() : Ok(circles);
    }

    /// <summary>
    /// Get circle from database by id
    /// </summary>
    /// <param name="id">Circle's id</param>
    /// <returns>Circle</returns>
    [HttpGet(CirclesRoutes.Id)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult<Circle>> GetCircleAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /circles/{id}");

        return GetEntryAsync<Circle>(id);
    }

    /// <summary>
    /// Get collection of all circles's titles
    /// </summary>
    /// <returns>Collection of CirclesTitles</returns>
    [HttpGet(CirclesRoutes.AllTitles)]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<CirclesTitles>> GetAllTitles()
    {
        Console.WriteLine($"Enter into GET: /circles/titles");

        var titles = ContextHelper.GetAllTitles();

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
        Console.WriteLine($"Enter into GET: /circles/{id}/titles");

        var titles = await ContextHelper.GetTitlesAsync(id).ConfigureAwait(false);

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
        Console.WriteLine($"Enter into GET: /circles/{id}/authors");

        var authors = await ContextHelper.GetAuthorsAsync(id).ConfigureAwait(false);

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
        Console.WriteLine($"Enter into GET: /circles/{id}/creations");

        var creations = await ContextHelper.GetCreationsAsync(id).ConfigureAwait(false);

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
        Console.WriteLine($"Enter into GET: /circles/{id}/tags");

        var tags = await ContextHelper.GetTagsAsync(id).ConfigureAwait(false);

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

        Console.WriteLine("Enter into POST: /circles");

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
        Console.WriteLine($"Enter into POST: /circles/{id}/titles");

        var isSuccess = await ContextHelper.AddTitlesAsync(id, titles).ConfigureAwait(false);

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
        Console.WriteLine($"Enter into PUT: /circles/{id}/authors");

        var isSuccess = await ContextHelper.AddAuthorsAsync(id, authorsIds).ConfigureAwait(false);

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
        Console.WriteLine($"Enter into PUT: /circles/{id}/creations");

        var isSuccess = await ContextHelper.AddCreationsAsync(id, creationIds).ConfigureAwait(false);

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
        Console.WriteLine($"Enter into PUT: /circles/{id}/tags");

        var isSuccess = await ContextHelper.AddTagsAsync(id, tagIds).ConfigureAwait(false);

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
    public Task<ActionResult> DeleteCircleAsync(ulong id)
    {
        Console.WriteLine($"Enter into DELETE: /circles/{id}");

        return DeleteEntryAsync<Circle>(id);
    }

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
        Console.WriteLine($"Enter into DELETE: /circles/{id}/titles");

        var isSuccess = await ContextHelper.RemoveTitlesAsync(id, titleIds).ConfigureAwait(false);

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
        Console.WriteLine($"Enter into DELETE: /circles/{id}/authors");

        var isSuccess = await ContextHelper.RemoveAuthorsAsync(id, authorIds).ConfigureAwait(false);

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
        Console.WriteLine($"Enter into DELETE: /circles/{id}/creations");

        var isSuccess = await ContextHelper.RemoveCreationsAsync(id, creationIds).ConfigureAwait(false);

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
        Console.WriteLine($"Enter into DELETE: /circles/{id}/tags");

        var isSuccess = await ContextHelper.RemoveTagsAsync(id, tagIds).ConfigureAwait(false);

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
    ///         "path": "/age",
    ///         "op": "replace",
    ///         "value": 30
    ///     },
    ///     {
    ///         "path": "/circlenames",
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
    public Task<ActionResult> PatchCircleAsync(ulong id, IEnumerable<Operation<Circle>> operations)
    {
        Console.WriteLine($"Enter into PATCH: /circles/{id}");

        return PatchEntryAsync(id, operations);
    }

    #endregion

    #endregion
}
