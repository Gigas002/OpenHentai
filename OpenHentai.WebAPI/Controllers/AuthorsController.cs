using System.Net.Mime;
using OpenHentai;
using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch;
using SystemTextJsonPatch.Operations;
using OpenHentai.Creatures;
using Microsoft.EntityFrameworkCore;

namespace OpenHentai.WebAPI.Controllers;

// TODO: https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-8-preview-5/#support-for-generic-attributes

/// <summary>
/// 
/// </summary>
// [AutoValidateAntiforgeryToken]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("/")]
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
    [HttpGet("authors/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Author>> GetAuthorAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}");

        var authors = _context.Authors
            .Include(a => a.AuthorsNames)
            .Include(a => a.Circles)
            .Include(a => a.AuthorsCreations)
            .ThenInclude(ac => ac.Related)
            .Include(a => a.CreaturesNames)
            .Include(a => a.Tags)
            .Include(a => a.CreaturesRelations)
            .ThenInclude(cr => cr.Related);

        var author = await authors.FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);

        // var author = await _context.Authors.FindAsync(id).ConfigureAwait(false);

        if (author is null)
            return BadRequest(new ProblemDetails { Detail = $"Author with id={id} doesn't exist" });
        else
            return Ok(author);
    }

    #endregion

    #endregion
}
