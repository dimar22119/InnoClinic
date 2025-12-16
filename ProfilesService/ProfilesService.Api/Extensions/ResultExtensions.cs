using Microsoft.AspNetCore.Mvc;
using ProfilesService.Application.Common;

namespace ProfilesService.Api.Extensions
{
    public static class ResultExtensions
    {
        public static ActionResult<T> ToActionResult<T>(this Result<T> result, ControllerBase controller)
        {
            if (result.IsSuccess)
                return controller.Ok(result.Value);

            return result.Error!.Code switch
            {
                ErrorCode.NotFound => controller.NotFound(new { error = result.Error.Message }),
                ErrorCode.ValidationFailed => controller.BadRequest(new { error = result.Error.Message }),
                ErrorCode.Conflict => controller.Conflict(new { error = result.Error.Message }),
                _ => controller.StatusCode(500, new { error = result.Error.Message })
            };
        }

        public static IActionResult ToActionResult(this Result result, ControllerBase controller)
        {
            if (result.IsSuccess)
                return controller.NoContent();

            return result.Error!.Code switch
            {
                ErrorCode.NotFound => controller.NotFound(new { error = result.Error.Message }),
                ErrorCode.ValidationFailed => controller.BadRequest(new { error = result.Error.Message }),
                ErrorCode.Conflict => controller.Conflict(new { error = result.Error.Message }),
                _ => controller.StatusCode(500, new { error = result.Error.Message })
            };
        }
    }
}
