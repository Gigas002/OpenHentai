using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OpenHentai.Creatures;
using OpenHentai.Contexts;
using SystemTextJsonPatch;

namespace OpenHentai.WebAPI.Controllers;

// TODO: https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-8-preview-5/#support-for-generic-attributes

/// <summary>
/// 
/// </summary>
// [AutoValidateAntiforgeryToken]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("/authors")]
public class AuthorController : ControllerBase
{
    #region Properties

    private readonly DatabaseContext _context;

    #endregion

    #region Constructors

    /// <summary>
    /// Database context
    /// </summary>
    public AuthorController(DatabaseContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    #region GET

    // GET: authors/5
    /// <summary>
    /// Get author from database by id
    /// </summary>
    /// <param name="id">Author's id</param>
    /// <returns>Author</returns>
    /// <response code="200">Returns requested author</response>
    /// <response code="400">Author is null</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Author>> GetAuthorAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}");

        var author = await AuthorsContext.GetAuthorWithPropsAsync(_context, id).ConfigureAwait(false);

        if (author is null)
            return BadRequest(new ProblemDetails { Detail = $"Author with id={id} doesn't exist" });
        else
            return Ok(author);
    }
    
    #endregion

    #region POST

    // POST: authors/
    /// <summary>
    /// Add author to database
    /// </summary>
    /// <param name="author">Author to add</param>
    /// <returns>Created author</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /authors/
    ///     {
    ///        "Name": "Mikhail",
    ///        "Age": 69
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns the created author</response>
    /// <response code="400">Author is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Author>> PostUserAsync(Author author)
    {
        Console.WriteLine("Enter into POST: /authors/");

        if (author == null) return BadRequest();

        await AuthorsContext.AddAuthorAsync(_context, author).ConfigureAwait(false);

        return Ok(author);
    }

    #endregion

    // #region DELETE
    //
    // // DELETE: authors/1
    // /// <summary>
    // /// Delete author
    // /// </summary>
    // /// <param name="id">Id of author to delete</param>
    // /// <returns>Deleted author</returns>
    // /// <response code="200">Returns the removed author</response>
    // [HttpDelete("{id}")]
    // [Produces(MediaTypeNames.Application.Json)]
    // public async Task<ActionResult> DeleteAuthorAsync(ulong id)
    // {
    //     Console.WriteLine($"Enter into DELETE: /authors/{id}");
    //
    //     var author = await AuthorsContext.DeleteAuthorAsync(_context, id).ConfigureAwait(false);
    //
    //     return Ok(author);
    // }
    //
    // #endregion
    //
    // #region PUT
    //
    // // PUT: authors/1
    // /// <summary>
    // /// Update author
    // /// </summary>
    // /// <param name="id">Id of author to update</param>
    // /// <param name="author">Updated for author</param>
    // /// <returns>A created author</returns>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     PUT /authors/1
    // ///     {
    // ///         "name": "Petka",
    // ///         "age": 88
    // ///     }
    // ///
    // /// </remarks>
    // /// <response code="200">Returns the created author</response>
    // /// <response code="400">New author is null</response>
    // [HttpPut("{id}")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [Consumes(MediaTypeNames.Application.Json)]
    // [Produces(MediaTypeNames.Application.Json)]
    // public async Task<ActionResult<Author>> PutUserAsync(ulong id, Author author)
    // {
    //     Console.WriteLine($"Enter into PUT: /authors/{id}");
    //
    //     await AuthorsContext.UpdateAuthorAsync(_context, id, author).ConfigureAwait(false);
    //
    //     return Ok(author);
    // }
    //
    // #endregion
    //
    // #region PATCH
    //
    // // PATCH: patch/1
    // /// <summary>
    // /// Patch user
    // /// </summary>
    // /// <param name="id">Id of user to patch</param>
    // /// <param name="patch">Patch to apply</param>
    // /// <returns>A newly created User</returns>
    // /// <remarks>
    // /// Sample request:
    // ///
    // ///     PATCH /patch/1
    // ///     [
    // ///         {
    // ///             "op": "replace",
    // ///             "path": "/name",
    // ///             "value": "Greck"
    // ///         },
    // ///         {
    // ///             "op": "replace",
    // ///             "path": "/age",
    // ///             "value": 51
    // ///         }
    // ///     ]
    // ///
    // /// </remarks>
    // /// <response code="201">Returns the newly created user</response>
    // /// <response code="400">Patch is null</response>
    // [HttpPatch("authors/{id}")]
    // [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // [Consumes("application/json-patch+json")]
    // [Produces(MediaTypeNames.Application.Json)]
    // public async Task<ActionResult<Author>> PatchAuthorAsync(ulong id, JsonPatchDocument<Author> patch)
    // {
    //     Console.WriteLine($"Enter into PATCH: /authors/{id}");
    //
    //     var authorToUpdate = await AuthorsContext.GetAuthorAsync(_context, id);
    //     // var update = new Author();
    //
    //     if (patch is null) return BadRequest();
    //
    //     patch.ApplyTo(authorToUpdate);
    //
    //     await AuthorsContext.UpdateAuthorAsync(_context, id, authorToUpdate).ConfigureAwait(false);
    //
    //     return Ok(authorToUpdate);
    // }
    //
    // #endregion

    #endregion
}
