using BLL.DTOs;
using BLL.Services.Players.Application;
using Microsoft.AspNetCore.Mvc;

namespace WebChattingServer.Controllers
{
    [Route("player/partner")]
    public class PlayerPartnerController : PlayerApiControllerBase
    {
        private readonly IPlayerApplicationService _playerApplicationService;

        public PlayerPartnerController(IPlayerApplicationService playerApplicationService)
        {
            _playerApplicationService = playerApplicationService;
        }

        [HttpPost("level-up")]
        public async Task<IActionResult> LevelUpPartner([FromBody] LevelUpPartnerDTO partnerDTO)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.LevelUpPartnerAsync(playerId, partnerDTO));
        }

        [HttpPost("add-amount")]
        public async Task<IActionResult> AddPartnerAmount([FromBody] PartnerAmountDTO partnerDTO)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.AddPartnerAmountAsync(playerId, partnerDTO));
        }

        [HttpPost("equip")]
        public async Task<IActionResult> EquipPartner([FromBody] PartnerEquipDTO partnerDTO)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.EquipPartnerAsync(playerId, partnerDTO));
        }

        [HttpPost("set-upgrade-and-amount")]
        public async Task<IActionResult> SetUpgradeAndAmount([FromBody] SetPartnerAmountAndUpgradeDTO dto)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.SetPartnerProgressAsync(playerId, dto));
        }
    }
}
