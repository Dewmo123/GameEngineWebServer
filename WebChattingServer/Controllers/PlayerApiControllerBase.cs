using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebChattingServer.Controllers
{
    [Authorize]
    public abstract class PlayerApiControllerBase : ApiResultControllerBase
    {
        protected bool TryGetCurrentPlayerId(out int playerId, out IActionResult? error)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrWhiteSpace(id) && int.TryParse(id, out playerId))
            {
                error = null;
                return true;
            }

            playerId = default;
            error = Unauthorized();
            return false;
        }
    }
}
