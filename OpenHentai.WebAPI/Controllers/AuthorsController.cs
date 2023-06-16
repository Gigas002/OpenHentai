using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OpenHentai.Creatures;
using OpenHentai.Contexts;

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

        var author = await AuthorsContext.GetAuthorAsync(_context, id).ConfigureAwait(false);

        if (author is null)
            return BadRequest(new ProblemDetails { Detail = $"Author with id={id} doesn't exist" });
        else
            return Ok(author);
    }

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
    /// <response code="201">Returns the created author</response>
    /// <response code="400">Author is null</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Author>> PostUserAsync(Author author)
    {
        Console.WriteLine("Enter into POST: /authors/");

        if (author == null) return BadRequest();

        await AuthorsContext.AddAuthorAsync(_context, author).ConfigureAwait(false);

        return CreatedAtAction(nameof(GetAuthorAsync), new { id = author.Id }, author);
    }

    #endregion

    #region DELETE

    // DELETE: authors/1
    /// <summary>
    /// Delete author
    /// </summary>
    /// <param name="id">Id of author to delete</param>
    /// <returns>Deleted author</returns>
    /// <response code="200">Returns the removed author</response>
    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteAuthorAsync(ulong id)
    {
        Console.WriteLine($"Enter into DELETE: /authors/{id}");

        var author = await AuthorsContext.DeleteAuthorAsync(_context, id).ConfigureAwait(false);

        return Ok(author);
    }

    #endregion

    #endregion

    #endregion
}
