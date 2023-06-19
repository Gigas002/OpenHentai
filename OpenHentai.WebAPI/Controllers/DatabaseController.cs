using Microsoft.AspNetCore.Mvc;
using OpenHentai.Contexts;

// TODO: disposable

public abstract class DatabaseController : ControllerBase
{
    #region Properties

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

    #endregion
}
