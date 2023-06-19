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

        var author = await AuthorsContext.GetAuthorWithPropsAsync(Context, id).ConfigureAwait(false);

        if (author is null)
            return BadRequest(new ProblemDetails { Detail = $"Author with id={id} doesn't exist" });
        else
            return Ok(author);
    }

    [HttpGet("/authors_names")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<AuthorsNames>> GetAuthorsNames()
    {
        Console.WriteLine($"Enter into GET: /authors/names");

        var names = Context.AuthorsNames.Include(an => an.Entity).ToList();

        return names;
    }

    [HttpGet("{id}/author_names")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<AuthorsNames>> GetAuthorNames(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/names");

        var author = Context.Authors.Include(a => a.AuthorsNames)
                             .FirstOrDefault(a => a.Id == id);

        return author.AuthorsNames;
    }

    [HttpGet("{id}/circles")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<Circle>> GetAuthorCircles(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/circles");

        var author = Context.Authors.Include(a => a.Circles)
                             .FirstOrDefault(a => a.Id == id);

        return author.Circles;
    }

    [HttpGet("{id}/creations")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<AuthorsCreations>> GetAuthorCreations(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/creations");

        var author = Context.Authors.Include(a => a.AuthorsCreations)
                             .ThenInclude(ac => ac.Related)
                             .FirstOrDefault(a => a.Id == id);

        return author.AuthorsCreations;
    }

    [HttpGet("/{id}/names")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<CreaturesNames>> GetCreatureNames(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/names");

        var author = Context.Authors.Include(a => a.CreaturesNames)
                                   .FirstOrDefault(a => a.Id == id);

        return Ok(author.CreaturesNames);
    }

    [HttpGet("/{id}/tags")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<Tag>> GetCreatureTags(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/tags");

        var author = Context.Authors.Include(a => a.Tags)
                                    .FirstOrDefault(a => a.Id == id);

        return Ok(author.Tags);
    }

    [HttpGet("/{id}/relations")]
    [Produces(MediaTypeNames.Application.Json)]
    public ActionResult<IEnumerable<CreaturesRelations>> GetCreatureRelations(ulong id)
    {
        Console.WriteLine($"Enter into GET: /authors/{id}/relations");

        var author = Context.Authors.Include(a => a.CreaturesRelations)
                                    .ThenInclude(cr => cr.Related)
                                    .FirstOrDefault(a => a.Id == id);

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

        await AuthorsContext.AddAuthorAsync(Context, author).ConfigureAwait(false);

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
    //     var author = await AuthorsContext.DeleteAuthorAsync(Context, id).ConfigureAwait(false);
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
    //     await AuthorsContext.UpdateAuthorAsync(Context, id, author).ConfigureAwait(false);
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
