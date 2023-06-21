namespace OpenHentai.Contexts;

public abstract class DatabaseContextHelper : IDisposable, IAsyncDisposable
{
    #region Properties

    protected bool IsDisposed { get; set; }

    public DatabaseContext Context { get; init; }

    #endregion

    #region Constructors

    protected DatabaseContextHelper(DatabaseContext context) => Context = context;

    ~DatabaseContextHelper() => Dispose(false);

    #endregion

    #region Methods

    #region Dispose

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
