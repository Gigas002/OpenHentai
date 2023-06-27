using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace OpenHentai.WebAPI.Tests;

public static class Global
{
    public static bool CheckResponse<T>(ActionResult<T> response) where T : class
    {
        var statuscode = GetStatusCode(response);

        return statuscode == 200 || statuscode == 201;
    }

    public static int? GetStatusCode<T>(ActionResult<T?> actionResult)
    {
        // see: https://stackoverflow.com/questions/73594323/how-to-get-actionresult-statuscode-in-asp-net-core

        IConvertToActionResult convertToActionResult = actionResult;
        var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;

        return actionResultWithStatusCode?.StatusCode;
    }
}