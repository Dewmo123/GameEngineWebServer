using BLL.DTOs;
using BLL.Services.Players.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebChattingServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("player")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerSessionService _playerSessionService;

        public PlayerController(IPlayerSessionService playerSessionService)
        {
            _playerSessionService = playerSessionService;
        }

        [HttpGet("get-player-infos")]
        public async Task<ActionResult<PlayerDTO>> GetPlayerInfos()
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
                return await _playerSessionService.LoadPlayerAsync(val);

            return NoContent();
        }

        [HttpDelete("log-out")]
        public async Task LogOut()
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
                await _playerSessionService.UnloadPlayerAsync(val);
        }
    }
}
