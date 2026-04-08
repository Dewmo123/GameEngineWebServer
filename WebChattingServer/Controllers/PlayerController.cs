using BLL.DTOs;
using BLL.Services.Players.Application;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WebChattingServer.Controllers
{
    [Route("player")]
    public class PlayerController : PlayerApiControllerBase
    {
        private readonly IPlayerApplicationService _playerApplicationService;

        public PlayerController(IPlayerApplicationService playerApplicationService)
        {
            _playerApplicationService = playerApplicationService;
        }

        [HttpGet("get-player-infos")]
        public async Task<IActionResult> GetPlayerInfos()
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.GetPlayerAsync(playerId));
        }

        [HttpDelete("log-out")]
        public async Task<IActionResult> LogOut()
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            var result = await _playerApplicationService.LogOutAsync(playerId);
            if (!result.Succeeded)
                return ToActionResult(result);

            await HttpContext.SignOutAsync("UserKey");
            return Ok();
        }
    }
}
