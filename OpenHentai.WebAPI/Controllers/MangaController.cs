using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Creations;

namespace OpenHentai.WebAPI.Controllers;

// TODO: https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-8-preview-5/#support-for-generic-attributes

/// <summary>
/// 
/// </summary>
// [AutoValidateAntiforgeryToken]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("/manga")]
public class MangaController : ControllerBase
{
    #region Properties

    private readonly DatabaseContext _context;

    #endregion

    #region Constructors

    /// <summary>
    /// Database context
    /// </summary>
    public MangaController(DatabaseContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    #region GET

    // GET: manga/5
    /// <summary>
    /// Get manga from database by id
    /// </summary>
    /// <param name="id">Manga's id</param>
    /// <returns>Manga</returns>
    /// <response code="200">Returns requested manga</response>
    /// <response code="400">Manga is null</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Manga>> GetMangaAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /manga/{id}");

        var manga = _context.Manga.Include(m => m.CreationsRelations)
            .Include(m => m.CreationsTitles)
            .Include(m => m.AuthorsCreations)
            .ThenInclude(ac => ac.Origin)
            .Include(m => m.Circles)
            .Include(m => m.CreationsCharacters)
            .ThenInclude(cc => cc.Related)
            .Include(m => m.Tags);

        var m = await manga.FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);

        if (m is null)
            return BadRequest(new ProblemDetails { Detail = $"Author with id={id} doesn't exist" });
        else
            return Ok(m);
    }

    #endregion

    #endregion
}
