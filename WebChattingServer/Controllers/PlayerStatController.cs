using BLL.DTOs;
using BLL.Services.Players.Application;
using Microsoft.AspNetCore.Mvc;

namespace WebChattingServer.Controllers
{
    [Route("player/stat")]
    public class PlayerStatController : PlayerApiControllerBase
    {
        private readonly IPlayerApplicationService _playerApplicationService;

        public PlayerStatController(IPlayerApplicationService playerApplicationService)
        {
            _playerApplicationService = playerApplicationService;
        }

        [HttpPost("level-up")]
        public async Task<IActionResult> StatLevelUp([FromBody] StatDTO statDTO)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.LevelUpStatAsync(playerId, statDTO));
        }
    }
}
