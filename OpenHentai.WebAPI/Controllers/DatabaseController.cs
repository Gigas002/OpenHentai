using Microsoft.AspNetCore.Mvc;
using SystemTextJsonPatch.Operations;
using SystemTextJsonPatch;
using OpenHentai.Contexts;

namespace OpenHentai.WebAPI.Controllers;

public abstract class DatabaseController<T> : ControllerBase, IDisposable, IAsyncDisposable
    where T : DatabaseContextHelper, IDisposable, IAsyncDisposable
{
    #region Properties/fields

    protected bool IsDisposed { get; set; }

    public T ContextHelper { get; init; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initialize database context
    /// </summary>
    protected DatabaseController(T contextHelper) => ContextHelper = contextHelper;

    ~DatabaseController() => Dispose(false);

    #endregion

    #region Methods

    public async Task<ActionResult<TEntry>> GetEntryAsync<TEntry>(ulong id) where TEntry : class, IDatabaseEntity
    {
        var entry = await ContextHelper.GetEntryAsync<TEntry>(id).ConfigureAwait(false);

        return entry is null ? NotFound() : Ok(entry);
    }

    public async Task<bool> PostEntryAsync<TEntry>(TEntry entry) where TEntry : class, IDatabaseEntity
    {
        if (entry is null) throw new ArgumentNullException(nameof(entry));

        var isSuccess = await ContextHelper.AddEntryAsync(entry).ConfigureAwait(false);

        return isSuccess;
    }

    public async Task<ActionResult> DeleteEntryAsync<TEntry>(ulong id) where TEntry : class, IDatabaseEntity
    {
        var isSuccess = await ContextHelper.RemoveEntryAsync<TEntry>(id).ConfigureAwait(false);

        return isSuccess ? Ok() : BadRequest();
    }

    public async Task<ActionResult> PatchEntryAsync<TEntry>(ulong id,
        IEnumerable<Operation<TEntry>> operations) where TEntry : class, IDatabaseEntity
    {
        var patch = new JsonPatchDocument<TEntry>(operations.ToList(), Essential.JsonSerializerOptions);

        var entry = await ContextHelper.GetEntryAsync<TEntry>(id).ConfigureAwait(false);

        if (entry is null) return BadRequest();

        patch.ApplyTo(entry);

        await ContextHelper.Context.SaveChangesAsync().ConfigureAwait(false);

        return Ok();
    }

    #region Dispose

    [ApiExplorerSettings(IgnoreApi = true)]
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed) return;

        if (disposing)
        { }

        ContextHelper.Dispose();

        IsDisposed = true;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        Dispose(false);
        GC.SuppressFinalize(this);
    }

#pragma warning disable CS1998
    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (IsDisposed) return;
        
        IsDisposed = true;
    }
#pragma warning restore CS1998

    #endregion

    #endregion
}
