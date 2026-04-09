using BLL.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace WebChattingServer.Controllers
{
    [ApiController]
    public abstract class ApiResultControllerBase : ControllerBase
    {
        protected IActionResult ToActionResult(Result result)
        {
            return result.Status switch
            {
                ResultStatus.Success => Ok(result.Message),
                ResultStatus.Created => StatusCode(StatusCodes.Status201Created, result.Message),
                ResultStatus.Unauthorized => Unauthorized(new { Message = result.Message }),
                ResultStatus.NotFound => NotFound(new { Message = result.Message }),
                ResultStatus.Conflict => Conflict(new { Message = result.Message }),
                ResultStatus.PersistenceFailure => StatusCode(StatusCodes.Status500InternalServerError, new { Message = result.Message }),
                _ => BadRequest(new { Message = result.Message })
            };
        }

        protected IActionResult ToActionResult<T>(Result<T> result)
        {
            return result.Status switch
            {
                ResultStatus.Success => Ok(result.Value),
                ResultStatus.Created => StatusCode(StatusCodes.Status201Created, result.Value),
                ResultStatus.Unauthorized => Unauthorized(new { Message = result.Message }),
                ResultStatus.NotFound => NotFound(new { Message = result.Message }),
                ResultStatus.Conflict => Conflict(new { Message = result.Message }),
                ResultStatus.PersistenceFailure => StatusCode(StatusCodes.Status500InternalServerError, new { Message = result.Message }),
                _ => BadRequest(new { Message = result.Message })
            };
        }
    }
}
