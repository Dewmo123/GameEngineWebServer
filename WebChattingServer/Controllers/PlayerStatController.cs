using BLL.Caching;
using BLL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebChattingServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("player/stat")]
    public class PlayerStatController : ControllerBase
    {
        IPlayerManager _playerManager;
        public PlayerStatController(IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
        [HttpPost("level-up")]
        public IActionResult StatLevelUp(StatDTO statDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                bool success = _playerManager.GetPlayer(val).LevelUpStat(statDTO.StatType, statDTO.Level); ;
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }
    }
}
