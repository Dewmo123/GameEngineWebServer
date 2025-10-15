using BLL.Caching;
using BLL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebChattingServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("player/stage")]
    public class PlayerStageController : ControllerBase
    {
        IPlayerManager _playerManager;
        public PlayerStageController(IPlayerManager manager)
        {
            _playerManager = manager;
        }
        [HttpPost("changed")]
        public IActionResult StageChanged(ChapterDTO chapter)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                bool success = _playerManager.GetPlayer(val).ChapterChanged(chapter.Chapter, chapter.Stage);
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }
        [HttpPost("enemy-dead")]
        public IActionResult EnemyDead(EnemyDeadDTO enemyDead)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                _playerManager.GetPlayer(val).EnemyDead(enemyDead.EnemyCount);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
