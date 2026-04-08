using BLL.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebChattingServer.Controllers
{
    [ApiController]
    public abstract class ApiResultControllerBase : ControllerBase
    {
        protected IActionResult ToActionResult(Result result)
        {
            if (result.Succeeded)
                return Ok();

            return result.Error!.Code switch
            {
                ErrorCode.Unauthorized => Unauthorized(new { Message = result.Error.Message }),
                ErrorCode.NotFound => NotFound(new { Message = result.Error.Message }),
                ErrorCode.Conflict => Conflict(new { Message = result.Error.Message }),
                ErrorCode.PersistenceFailure => StatusCode(StatusCodes.Status500InternalServerError, new { Message = result.Error.Message }),
                _ => BadRequest(new { Message = result.Error.Message })
            };
        }

        protected IActionResult ToActionResult<T>(Result<T> result)
        {
            if (result.Succeeded)
                return Ok(result.Value);

            IActionResult failure = result.Error!.Code switch
            {
                ErrorCode.Unauthorized => Unauthorized(new { Message = result.Error.Message }),
                ErrorCode.NotFound => NotFound(new { Message = result.Error.Message }),
                ErrorCode.Conflict => Conflict(new { Message = result.Error.Message }),
                ErrorCode.PersistenceFailure => StatusCode(StatusCodes.Status500InternalServerError, new { Message = result.Error.Message }),
                _ => BadRequest(new { Message = result.Error.Message })
            };

            return failure;
        }
    }
}
