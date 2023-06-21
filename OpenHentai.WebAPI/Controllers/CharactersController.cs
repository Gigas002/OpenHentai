using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OpenHentai.Creatures;
using OpenHentai.Contexts;
using SystemTextJsonPatch;
using OpenHentai.Relative;
using OpenHentai.Circles;
using OpenHentai.Tags;
using SystemTextJsonPatch.Operations;
using OpenHentai.Roles;
using OpenHentai.Relations;
using OpenHentai.Descriptors;
using OpenHentai.WebAPI.Constants;

namespace OpenHentai.WebAPI.Controllers;

// TODO: https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-8-preview-5/#support-for-generic-attributes

#pragma warning disable CA1303

/// <summary>
/// Controller, that works with Characters table and it's dependent ones
/// </summary>
// [AutoValidateAntiforgeryToken]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route(CharactersRoutes.Base)]
public class CharacterController : DatabaseController<CharactersContextHelper>, ICreatureController
{
    #region Constructors

    /// <inheritdoc/>
    public CharacterController(CharactersContextHelper contextHelper) : base(contextHelper) { }

    #endregion

    #region Methods

    #region GET

    /// <summary>
    /// Get all characters
    /// </summary>
    /// <returns>Collection of Characters</returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<Character>> GetCharacters()
    {
        Console.WriteLine($"Enter into GET: /characters");

        var characters = ContextHelper.GetCharacters();

        return characters is null ? NotFound() : Ok(characters);
    }

    /// <summary>
    /// Get character from database by id
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <returns>Character</returns>
    [HttpGet(CharactersRoutes.Id)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Character>> GetCharacterAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /characters/{id}");

        var character = await ContextHelper.GetCharacterAsync(id).ConfigureAwait(false);

        return character is null ? NotFound() : Ok(character);
    }

    /// <summary>
    /// Get current character's creations
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <returns>Collection of CharactersCreations</returns>
    [HttpGet(CharactersRoutes.Creations)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreationsCharacters>>> GetCreationsAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /characters/{id}/creations");

        var creations = await ContextHelper.GetCreationsAsync(id).ConfigureAwait(false);

        return creations is null ? NotFound() : Ok(creations);
    }

    /// <summary>
    /// Get current character's names
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <returns>Collection of CreaturesNames</returns>
    [HttpGet(CharactersRoutes.Names)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreaturesNames>>> GetNamesAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /characters/{id}/names");

        var names = await ContextHelper.GetNamesAsync(id).ConfigureAwait(false);

        return names is null ? NotFound() : Ok(names);
    }

    /// <summary>
    /// Get current character's tags
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <returns>Collection of Tag</returns>
    [HttpGet(CharactersRoutes.Tags)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTagsAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /characters/{id}/tags");

        var tags = await ContextHelper.GetTagsAsync(id).ConfigureAwait(false);

        return tags is null ? NotFound() : Ok(tags);
    }

    /// <summary>
    /// Get current character's relations
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <returns>Collection of CreaturesRelations</returns>
    [HttpGet(CharactersRoutes.Relations)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreaturesRelations>>> GetRelationsAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /characters/{id}/relations");

        var relations = await ContextHelper.GetRelationsAsync(id).ConfigureAwait(false);

        return relations is null ? NotFound() : Ok(relations);
    }

    #endregion

    #region POST

    /// <summary>
    /// Add character to database
    /// </summary>
    /// <param name="character">Character to add</param>
    /// <remarks>
    ///
    /// Minimal request:
    ///
    ///     POST /characters
    ///     { }
    ///
    /// </remarks>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Character>> PostCharacterAsync(Character character)
    {
        if (character is null) throw new ArgumentNullException(nameof(character));

        Console.WriteLine("Enter into POST: /characters");

        var isSuccess = await ContextHelper.AddCharacterAsync(character).ConfigureAwait(false);

        return isSuccess ? CreatedAtAction(nameof(GetCharacterAsync), new { id = character.Id }, character) : BadRequest();
    }

    /// <summary>
    /// Updates Character with new names, pushed at their own table
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <param name="names">Collection of new names to push</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     POST /characters/{id}/names
    ///     [{
    ///         "text": "Test Minato",
    ///         "language": null
    ///     }]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPost(CharactersRoutes.Names)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostNamesAsync(ulong id, HashSet<LanguageSpecificTextInfo> names)
    {
        Console.WriteLine($"Enter into POST: /characters/{id}/names");

        var isSuccess = await ContextHelper.AddNamesAsync(id, names).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Updates Character with new relations to other creatures
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <param name="relations">Dictionary of related creature id and relation type</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     POST /characters/{id}/relations
    ///     {
    ///         "1": 1,
    ///         "2": 0
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPost(CharactersRoutes.Relations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostRelationsAsync(ulong id, Dictionary<ulong, CreatureRelations> relations)
    {
        Console.WriteLine($"Enter into POST: /characters/{id}/relations");

        var isSuccess = await ContextHelper.AddRelationsAsync(id, relations).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region PUT

    /// <summary>
    /// Bind Character to creations
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <param name="creationRoles">Dictionary of creation ids and CharacterRole to bind with this Character</param>
    /// <remarks>
    ///
    /// Sample request:
    ///
    ///     PUT /characters/{id}/creations
    ///     {
    ///         "1": 3,
    ///         "4": 2
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(CharactersRoutes.Creations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutCreationsAsync(ulong id, Dictionary<ulong, CharacterRole> creationRoles)
    {
        Console.WriteLine($"Enter into PUT: /characters/{id}/creations");

        var isSuccess = await ContextHelper.AddCreationsAsync(id, creationRoles).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Bind Character to tags
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <param name="tagIds">Collection of tag ids to bind with this Character</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PUT /characters/{id}/tags
    ///     [
    ///         1, 2
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPut(CharactersRoutes.Tags)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        Console.WriteLine($"Enter into PUT: /characters/{id}/tags");

        var isSuccess = await ContextHelper.AddTagsAsync(id, tagIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region DELETE

    /// <summary>
    /// Delete Character from database
    /// </summary>
    /// <param name="id">Id of Character to delete</param>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(CharactersRoutes.Id)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteCharacterAsync(ulong id)
    {
        Console.WriteLine($"Enter into DELETE: /characters/{id}");

        var isSuccess = await ContextHelper.RemoveCharacterAsync(id).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Character and specified Creations
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <param name="creationIds">Collection of Creation ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /characters/{id}/creations
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(CharactersRoutes.Creations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteCreationsAsync(ulong id, HashSet<ulong> creationIds)
    {
        Console.WriteLine($"Enter into DELETE: /characters/{id}/creations");

        var isSuccess = await ContextHelper.RemoveCreationsAsync(id, creationIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete collection of names, bound to Character
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <param name="nameIds">Collection of CreturesNames ids to delete</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /characters/{id}/names
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(CharactersRoutes.Names)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteNamesAsync(ulong id, HashSet<ulong> nameIds)
    {
        Console.WriteLine($"Enter into DELETE: /characters/{id}/names");

        var isSuccess = await ContextHelper.RemoveNamesAsync(id, nameIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Character and specified Tags
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <param name="tagIds">Collection of Tag ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /characters/{id}/tags
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(CharactersRoutes.Tags)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteTagsAsync(ulong id, HashSet<ulong> tagIds)
    {
        Console.WriteLine($"Enter into DELETE: /characters/{id}/tags");

        var isSuccess = await ContextHelper.RemoveTagsAsync(id, tagIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    /// <summary>
    /// Delete binding between Character and specified Creatures
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <param name="relatedIds">Collection of Creature ids to delete binding</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     DELETE /characters/{id}/relations
    ///     [
    ///         1, 2    
    ///     ]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpDelete(CharactersRoutes.Relations)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteRelationsAsync(ulong id, HashSet<ulong> relatedIds)
    {
        Console.WriteLine($"Enter into DELETE: /characters/{id}/relations");

        var isSuccess = await ContextHelper.RemoveRelationsAsync(id, relatedIds).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    #endregion

    #region PATCH

    /// <summary>
    /// Update Character, using json-patch format
    /// </summary>
    /// <param name="id">Character's id</param>
    /// <param name="operations">Collection of json-patch operations</param>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     PATCH /characters/{id}
    ///     [{
    ///         "path": "/age",
    ///         "op": "replace",
    ///         "value": 30
    ///     },
    ///     {
    ///         "path": "/names",
    ///         "op": "add",
    ///         "value": [{
    ///           "character_id": 8,
    ///           "name": "Test Bubato",
    ///           "language": null
    ///         }]
    ///     }]
    ///
    /// </remarks>
    /// <response code="200">Complete</response>
    /// <response code="400">Entity with requested id doesn't exist</response>
    [HttpPatch(CharactersRoutes.Id)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypes.JsonPatch)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PatchCharacterAsync(ulong id, IEnumerable<Operation<Character>> operations)
    {
        Console.WriteLine($"Enter into PATCH: /characters/{id}");

        var patch = new JsonPatchDocument<Character>(operations.ToList(), Essential.JsonSerializerOptions);

        var character = await ContextHelper.GetCharacterAsync(id).ConfigureAwait(false);

        if (character is null) return BadRequest();

        patch.ApplyTo(character);

        await ContextHelper.Context.SaveChangesAsync().ConfigureAwait(false);

        return Ok();
    }

    #endregion

    #endregion
}
