using Microsoft.AspNetCore.Mvc;
using OpenHentai.Contexts;
using OpenHentai.WebAPI.Constants;

namespace OpenHentai.WebAPI.Controllers;

[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
[Route(CirclesRoutes.Base)]
public class CirclesController : DatabaseController<CirclesContextHelper>
{
    #region Constructors

    /// <inheritdoc/>
    public CirclesController(CirclesContextHelper contextHelper) : base(contextHelper) { }

    #endregion
}
