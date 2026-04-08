using BLL.DTOs;
using BLL.Services.Players.Application;
using Microsoft.AspNetCore.Mvc;

namespace WebChattingServer.Controllers
{
    [Route("player/stage")]
    public class PlayerStageController : PlayerApiControllerBase
    {
        private readonly IPlayerApplicationService _playerApplicationService;

        public PlayerStageController(IPlayerApplicationService playerApplicationService)
        {
            _playerApplicationService = playerApplicationService;
        }

        [HttpPost("chapter-changed")]
        public async Task<IActionResult> ChapterChanged([FromBody] ChangeChapterDTO chapter)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.ChangeChapterAsync(playerId, chapter));
        }

        [HttpPost("stage-changed")]
        public async Task<IActionResult> StageChanged([FromBody] ChangeStageDTO chapter)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.ChangeStageAsync(playerId, chapter));
        }

        [HttpPost("enemy-dead")]
        public async Task<IActionResult> EnemyDead([FromBody] EnemyDeadDTO enemyDead)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.EnemyDeadAsync(playerId, enemyDead));
        }
    }
}
