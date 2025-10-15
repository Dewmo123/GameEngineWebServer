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
    [Route("player")]
    public class PlayerController : ControllerBase
    {
        IPlayerService _playerService;
        IPlayerManager _playerManager;
        public PlayerController(IPlayerService playerService, IPlayerManager playerManager)
        {
            _playerService = playerService;
            _playerManager = playerManager;
        }
        [HttpGet("get-player-infos")]
        public async Task<ActionResult<PlayerDTO?>> GetPlayerInfos()
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                PlayerDTO? playerInfo = await _playerService.GetPlayerInfos(val);
                if (playerInfo == null) return default;
                bool success = _playerManager.AddPlayer(val, playerInfo);
                Console.WriteLine($"AddPlayer: {success}");
                return playerInfo;
            }
            return NoContent();
        }
        [HttpDelete("log-out")]
        public async Task LogOut()
        {
            Console.WriteLine("ASD");
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                bool success = _playerManager.RemovePlayer(val, out Player? player);
                if (success && player != null)
                {
                    success &= await _playerService.UpdatePlayer(player.Id, player.GetCopyDTO());
                }
                Console.WriteLine($"RemovePlayer: {success}");
            }
        }
    }
}
