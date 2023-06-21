using Microsoft.AspNetCore.Mvc;
using OpenHentai.Contexts;

public abstract class DatabaseController : ControllerBase, IDisposable, IAsyncDisposable
{
    #region Properties/fields

    protected bool IsDisposed { get; set; } = false;

    public DatabaseContext Context { get; init; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initialize database context
    /// </summary>
    public DatabaseController(DatabaseContext context)
    {
        Context = context;
    }

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

        Context.Dispose();

        IsDisposed = true;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();

        Dispose(false);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (IsDisposed) return;
        
        IsDisposed = true;
    }

    ~DatabaseController() => Dispose(false);

    #endregion
}
