using BLL.DTOs;
using BLL.Services.Players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace WebChattingServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("player")]
    public class PlayerController : ControllerBase
    {
        IPlayerService _playerService;
        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        [HttpGet("get-player-infos")]
        public async Task<ActionResult<PlayerDTO?>> GetPlayerInfos()
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(id);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id,out int val))
            {
                PlayerDTO? playerInfo = await _playerService.GetPlayerInfos(val);
                return playerInfo;
            }
            return NoContent();
        } 
    }
}
