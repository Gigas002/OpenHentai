using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch.Operations;
using OpenHentai.Creatures;
using OpenHentai.Contexts;
using OpenHentai.Relative;
using OpenHentai.Circles;
using OpenHentai.Tags;
using OpenHentai.Roles;
using OpenHentai.Relations;
using OpenHentai.Descriptors;
using OpenHentai.WebAPI.Constants;

namespace OpenHentai.WebAPI.Controllers;

// TODO: https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-8-preview-5/#support-for-generic-attributes

#pragma warning disable CA1303

/// <summary>
/// Controller, that works with Author table and it's dependent ones
/// </summary>
// [AutoValidateAntiforgeryToken]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route(AuthorsRoutes.Base)]
public class AuthorsController : DatabaseController<AuthorsContextHelper>, ICreaturesController
{
    #region Constructors

    /// <inheritdoc/>
    public AuthorsController(AuthorsContextHelper contextHelper) : base(contextHelper) { }

    #endregion

    #region Methods

    #region GET

    /// <summary>
    /// Get all authors
    /// </summary>
    /// <returns>Collection of Authors</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<Author>> GetAuthors()
    {
        var authors = ContextHelper.GetAuthors();

        return authors is null ? NotFound() : Ok(authors);
    }

    /// <summary>
    /// Get author from database by id
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <returns>Author</returns>
    [HttpGet(AuthorsRoutes.Id)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult<Author>> GetAuthorAsync(ulong id) => GetEntryAsync<Author>(id);

    /// <summary>
    /// Get collection of all authors's names
    /// </summary>
    /// <returns>Collection of AuthorsNames</returns>
    [HttpGet(AuthorsRoutes.AuthorsNames)]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<AuthorsNames>> GetAuthorsNames()
    {
        var names = ContextHelper.GetAuthorsNames();

        return names is null ? NotFound() : Ok(names);
    }

    /// <summary>
    /// Get current author's AuthorsNames
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <returns>Collection of AuthorsNames</returns>
    [HttpGet(AuthorsRoutes.AuthorNames)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<AuthorsNames>>> GetAuthorNamesAsync(ulong id)
    {
        var names = await ContextHelper.GetAuthorNamesAsync(id).ConfigureAwait(false);

        return names is null ? NotFound() : Ok(names);
    }

    /// <summary>
    /// Get current author's circles
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <returns>Collection of Circle</returns>
    [HttpGet(AuthorsRoutes.Circles)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Circle>>> GetCirclesAsync(ulong id)
    {
        var circles = await ContextHelper.GetCirclesAsync(id).ConfigureAwait(false);

        return circles is null ? NotFound() : Ok(circles);
    }

    /// <summary>
    /// Get current author's creations
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <returns>Collection of AuthorsCreations</returns>
    [HttpGet(AuthorsRoutes.Creations)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<AuthorsCreations>>> GetCreationsAsync(ulong id)
    {
        var creations = await ContextHelper.GetCreationsAsync(id).ConfigureAwait(false);

        return creations is null ? NotFound() : Ok(creations);
    }

    /// <summary>
    /// Get current author's real names
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <returns>Collection of CreaturesNames</returns>
    [HttpGet(AuthorsRoutes.Names)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreaturesNames>>> GetNamesAsync(ulong id)
    {
        var names = await ContextHelper.GetNamesAsync(id).ConfigureAwait(false);

        return names is null ? NotFound() : Ok(names);
    }

    /// <summary>
    /// Get current author's tags
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <returns>Collection of Tag</returns>
    [HttpGet(AuthorsRoutes.Tags)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTagsAsync(ulong id)
    {
        var tags = await ContextHelper.GetTagsAsync(id).ConfigureAwait(false);

        return tags is null ? NotFound() : Ok(tags);
    }

    /// <summary>
    /// Get current author's relations
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <returns>Collection of CreaturesRelations</returns>
    [HttpGet(AuthorsRoutes.Relations)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreaturesRelations>>> GetRelationsAsync(ulong id)
    {
        var relations = await ContextHelper.GetRelationsAsync(id).ConfigureAwait(false);

        return relations is null ? NotFound() : Ok(relations);
    }

    #endregion

    #region POST

    /// <summary>
    /// Add author to database
    /// </summary>
    /// <param name="author">Author to add</param>
    /// <remarks>
    ///
    /// Minimal request:
    ///
    ///     POST /authors
    ///     { }
    ///
    /// </remarks>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Author>> PostAuthorAsync(Author author)
    {
        var isSuccess = await PostEntryAsync(author).ConfigureAwait(false);

        return isSuccess ? CreatedAtAction(nameof(GetAuthorAsync), new { id = author.Id }, author) : BadRequest();
    }

    /// <summary>
    /// Updates Author with new AuthorsNames, pushed at their own table
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="names">Collection of new names to push</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     POST /authors/{id}/author_names
    ///     [{
    ///         "text": "taras panis",
    ///         "language": null
    ///     }]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPost(AuthorsRoutes.AuthorNames)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostAuthorNamesAsync(ulong id, HashSet<LanguageSpecificTextInfo> names)
    {
        var isSuccess = await ContextHelper.AddAuthorNamesAsync(id, names).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Updates Author with new names, pushed at their own table
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="names">Collection of new names to push</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     POST /authors/{id}/names
    ///     [{
    ///         "text": "Test Minato",
    ///         "language": null
    ///     }]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPost(AuthorsRoutes.Names)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostNamesAsync(ulong id, HashSet<LanguageSpecificTextInfo> names)
    {
        var isSuccess = await ContextHelper.AddNamesAsync(id, names).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Updates Author with new relations to other creatures
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="relations">Dictionary of related creature id and relation type</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     POST /authors/{id}/relations
    ///     {
    ///         "1": 1,
    ///         "2": 0
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPost(AuthorsRoutes.Relations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostRelationsAsync(ulong id, Dictionary<ulong, CreatureRelations> relations)
    {
        var isSuccess = await ContextHelper.AddRelationsAsync(id, relations).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region PUT

    /// <summary>
    /// Bind Author to collection of Circle
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="circleIds">Collection of Circle ids to bind with this Author</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PUT /authors/{id}/circles
    ///     [
    ///         1, 2
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(AuthorsRoutes.Circles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        var isSuccess = await ContextHelper.AddCirclesAsync(id, circleIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Bind Author to creations
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="creationRoles">Dictionary of creation ids and AuthorRole to bind with this Author</param>
    /// <remarks>
    ///
    /// Sample request:
    ///
    ///     PUT /authors/{id}/creations
    ///     {
    ///         "1": 3,
    ///         "4": 2
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(AuthorsRoutes.Creations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutCreationsAsync(ulong id, Dictionary<ulong, AuthorRole> creationRoles)
    {
        var isSuccess = await ContextHelper.AddCreationsAsync(id, creationRoles).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Bind Author to tags
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="tagIds">Collection of tag ids to bind with this Author</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PUT /authors/{id}/tags
    ///     [
    ///         1, 2
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(AuthorsRoutes.Tags)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        var isSuccess = await ContextHelper.AddTagsAsync(id, tagIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Delete Author from database
    /// </summary>
    /// <param name="id">Id of Author to delete</param>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(AuthorsRoutes.Id)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult> DeleteAuthorAsync(ulong id) => DeleteEntryAsync<Author>(id);

    /// <summary>
    /// Delete collection of AuthorsNames, bound to Author
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="nameIds">Collection of names ids to delete</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /authors/{id}/author_names
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(AuthorsRoutes.AuthorNames)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteAuthorNamesAsync(ulong id, HashSet<ulong> nameIds)
    {
        var isSuccess = await ContextHelper.RemoveAuthorNamesAsync(id, nameIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Author and specified Circles
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="circleIds">Collection of Circle ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /authors/{id}/circles
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(AuthorsRoutes.Circles)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteCirclesAsync(ulong id, HashSet<ulong> circleIds)
    {
        var isSuccess = await ContextHelper.RemoveCirclesAsync(id, circleIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Author and specified Creations
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="creationIds">Collection of Creation ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /authors/{id}/creations
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(AuthorsRoutes.Creations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteCreationsAsync(ulong id, HashSet<ulong> creationIds)
    {
        var isSuccess = await ContextHelper.RemoveCreationsAsync(id, creationIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete collection of names, bound to Author
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="nameIds">Collection of CreturesNames ids to delete</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /authors/{id}/names
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(AuthorsRoutes.Names)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteNamesAsync(ulong id, HashSet<ulong> nameIds)
    {
        var isSuccess = await ContextHelper.RemoveNamesAsync(id, nameIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Author and specified Tags
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="tagIds">Collection of Tag ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /authors/{id}/tags
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(AuthorsRoutes.Tags)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        var isSuccess = await ContextHelper.RemoveTagsAsync(id, tagIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Author and specified Creatures
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="relatedIds">Collection of Creature ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /authors/{id}/relations
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(AuthorsRoutes.Relations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteRelationsAsync(ulong id, HashSet<ulong> relatedIds)
    {
        var isSuccess = await ContextHelper.RemoveRelationsAsync(id, relatedIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region PATCH

    /// <summary>
    /// Update Author, using json-patch format
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <param name="operations">Collection of json-patch operations</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PATCH /authors/{id}
    ///     [{
    ///         "path": "/age",
    ///         "op": "replace",
    ///         "value": 30
    ///     },
    ///     {
    ///         "path": "/authornames",
    ///         "op": "add",
    ///         "value": [{
    ///           "author_id": 8,
    ///           "name": "Test Bubato",
    ///           "language": null
    ///         }]
    ///     }]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPatch(AuthorsRoutes.Id)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypes.JsonPatch)]
    [Produces(MediaTypeNames.Application.Json)]
    public Task<ActionResult> PatchAuthorAsync(ulong id, IEnumerable<Operation<Author>> operations) =>
        PatchEntryAsync(id, operations);

    #endregion

    #endregion
}
