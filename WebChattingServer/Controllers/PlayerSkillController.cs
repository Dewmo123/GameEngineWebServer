using BLL.DTOs;
using BLL.Services.Players.Application;
using Microsoft.AspNetCore.Mvc;

namespace WebChattingServer.Controllers
{
    [Route("player/skill")]
    public class PlayerSkillController : PlayerApiControllerBase
    {
        private readonly IPlayerApplicationService _playerApplicationService;

        public PlayerSkillController(IPlayerApplicationService playerApplicationService)
        {
            _playerApplicationService = playerApplicationService;
        }

        [HttpPost("level-up")]
        public async Task<IActionResult> LevelUpSkill([FromBody] LevelUpSkillDTO skillDTO)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.LevelUpSkillAsync(playerId, skillDTO));
        }

        [HttpPost("add-amount")]
        public async Task<IActionResult> AddSkillAmount([FromBody] SkillAmountDTO skillDTO)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.AddSkillAmountAsync(playerId, skillDTO));
        }

        [HttpPost("equip")]
        public async Task<IActionResult> EquipSkill([FromBody] SkillEquipDTO skillDTO)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.EquipSkillAsync(playerId, skillDTO));
        }

        [HttpPost("set-upgrade-and-amount")]
        public async Task<IActionResult> SetUpgradeAndAmount([FromBody] SetSkillAmountAndUpgradeDTO dto)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.SetSkillProgressAsync(playerId, dto));
        }
    }
}
