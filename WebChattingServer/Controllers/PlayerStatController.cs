using BLL.Caching;
using BLL.DTOs;
using BLL.Services.Players;
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
        private readonly IPlayerManager _playerManager;
        private readonly IPlayerStatService _playerStatService;
        public PlayerStatController(IPlayerManager playerManager, IPlayerStatService playerStatService)
        {
            _playerManager = playerManager;
            _playerStatService = playerStatService;
        }
        [HttpPost("level-up")]
        public IActionResult StatLevelUp(StatDTO statDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                Player player = _playerManager.GetPlayer(val);
                bool success = _playerStatService.LevelUpStat(player, statDTO.StatType, statDTO.Level);
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }
    }
}
