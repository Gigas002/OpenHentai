using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OpenHentai.Creatures;
using OpenHentai.Contexts;
using SystemTextJsonPatch;
using OpenHentai.Relative;
using Microsoft.EntityFrameworkCore;
using OpenHentai.Circles;
using OpenHentai.Creations;
using OpenHentai.Tags;
using SystemTextJsonPatch.Operations;

namespace OpenHentai.WebAPI.Controllers;

// TODO: https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-8-preview-5/#support-for-generic-attributes
// TODO: use async overloads where possible

/// <summary>
/// 123
/// </summary>
// [AutoValidateAntiforgeryToken]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route("/authors")]
public class AuthorController : DatabaseController, ICreatureController
{
    #region Constructors

    /// <inheritdoc/>
    public AuthorController(DatabaseContext context) : base(context) { }

    #endregion

    #region Methods

    #region GET

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<Author>> GetAuthors()
    {
        Console.WriteLine($"Enter into GET: /authors");

        var authors = Context.Authors;

        return Ok(authors);
    }

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

        var author = await AuthorsContext.GetAuthorAsync(Context, id).ConfigureAwait(false);

        if (author is null)
            return BadRequest(new ProblemDetails { Detail = $"Author with id={id} doesn't exist" });
        else
            return Ok(author);
    }

    [HttpGet("/authors_names")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<AuthorsNames>> GetAuthorsNames()
    {
        Console.WriteLine($"Enter into GET: /authors/authors_names");

        var names = Context.AuthorsNames.Include(an => an.Entity).ToList();

        return names;
    }

    [HttpGet("{id}/author_names")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<AuthorsNames>>> GetAuthorNamesAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/author_names");

        var author = await Context.Authors.Include(a => a.AuthorsNames)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        return author.AuthorsNames;
    }

    [HttpGet("{id}/circles")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Circle>>> GetAuthorCirclesAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/circles");

        var author = await Context.Authors.Include(a => a.Circles)
                                  .FirstOrDefaultAsync(a => a.Id == id);

        return author.Circles;
    }

    [HttpGet("{id}/creations")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<AuthorsCreations>>> GetAuthorCreationsAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/creations");

        var author = await Context.Authors.Include(a => a.AuthorsCreations)
                             .ThenInclude(ac => ac.Related)
                             .FirstOrDefaultAsync(a => a.Id == id);

        return author.AuthorsCreations;
    }

    [HttpGet("/{id}/names")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreaturesNames>>> GetCreatureNamesAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/names");

        var author = await Context.Authors.Include(a => a.CreaturesNames)
                                   .FirstOrDefaultAsync(a => a.Id == id);

        return Ok(author.CreaturesNames);
    }

    [HttpGet("/{id}/tags")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<Tag>>> GetCreatureTagsAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/tags");

        var author = await Context.Authors.Include(a => a.Tags)
                                    .FirstOrDefaultAsync(a => a.Id == id);

        return Ok(author.Tags);
    }

    [HttpGet("/{id}/relations")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<IEnumerable<CreaturesRelations>>> GetCreatureRelationsAsync(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/relations");

        var author = await Context.Authors.Include(a => a.CreaturesRelations)
                                    .ThenInclude(cr => cr.Related)
                                    .FirstOrDefaultAsync(a => a.Id == id);

        return Ok(author.CreaturesRelations);
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
    ///
    /// Minimal request:
    ///
    ///     POST /authors
    ///     { }
    ///
    /// </remarks>
    /// <response code="200">Returns the created author</response>
    /// <response code="400">Author is null</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<Author>> PostAuthorAsync(Author author)
    {
        Console.WriteLine("Enter into POST: /authors");

        if (author == null) return BadRequest();

        await AuthorsContext.AddAuthorAsync(Context, author).ConfigureAwait(false);

        return Ok(author);
    }

    /// <summary>
    /// Updates Author with NEW names, POSTed at their own table
    /// </summary>
    /// <remarks>
    ///
    /// Example request:
    ///
    ///     POST /authors/{id}/names
    ///     [{
    ///         "author_id": 9,
    ///         "name": "Test Minato",
    ///         "language": null
    ///     }]
    ///
    /// </remarks>
    [HttpPost("{id}/authors_names")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PostAuthorNamesAsync(ulong id, IEnumerable<AuthorsNames> names)
    {
        Console.WriteLine($"Enter into POST: /authors/{id}/authors_names");

        var author = await Context.Authors.FindAsync(id);
        
        foreach (var name in names)
        {
            author.AuthorsNames.Add(name);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    #endregion

    #region PUT

    // update author entry with EXISTING (posted) names, found by ids
    [HttpPut("{id}/authors_names")]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> PutAuthorNamesAsync(ulong id, IEnumerable<ulong> nameIds)
    {
        Console.WriteLine($"Enter into PUT: /authors/{id}/authors_names");

        var author = await Context.Authors.FindAsync(id);

        var names = new List<AuthorsNames>();

        foreach (var nameId in nameIds)
        {
            var name = await Context.AuthorsNames.FindAsync(nameId);

            names.Add(name);
        }
        
        foreach (var name in names)
        {
            author.AuthorsNames.Add(name);
        }

        await Context.SaveChangesAsync();

        return Ok();
    }

    #endregion

    #region DELETE

    [HttpDelete("{id}")]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult> DeleteAuthorAsync(ulong id)
    {
        Console.WriteLine($"Enter into DELETE: /authors/{id}");

        var author = await AuthorsContext.DeleteAuthorAsync(Context, id).ConfigureAwait(false);

        return Ok(author);
    }

    #endregion

    #region PATCH

    // TODO: requires more testing and improvements

    /// <remarks>
    /// Sample request:
    ///
    ///     PATCH /authors/{id}
    ///     [{
    ///         "path": "/age",
    ///         "op": "replace",
    ///         "value": 30
    ///     },
    ///     {
    ///         "path": "/authorsNames",
    ///         "op": "add",
    ///         "value": [{
    ///           "author_id": 8,
    ///           "name": "Test Bubato",
    ///           "language": null
    ///         }]
    ///     }]
    ///
    /// </remarks>
    [HttpPatch("{id}")]
    [Consumes("application/json-patch+json")]
    public async Task<ActionResult<Author>> PatchAuthorAsync(ulong id,
        IEnumerable<Operation<Author>> operations)
    {
        Console.WriteLine($"Enter into PATCH: /authors/{id}");

        var patch = new JsonPatchDocument<Author>(operations.ToList(), new());

        var user = await Context.Authors.FindAsync(id);

        patch.ApplyTo(user);

        await Context.SaveChangesAsync();

        return Ok();
    }

    #endregion

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
    //     var authorToUpdate = await AuthorsContext.GetAuthorAsync(Context, id);
    //     // var update = new Author();
    //
    //     if (patch is null) return BadRequest();
    //
    //     patch.ApplyTo(authorToUpdate);
    //
    //     await AuthorsContext.UpdateAuthorAsync(Context, id, authorToUpdate).ConfigureAwait(false);
    //
    //     return Ok(authorToUpdate);
    // }
    //
    // #endregion

    #endregion
}
