using BLL.Caching;
using BLL.DTOs;
using BLL.Services.Players;
using DAL.VOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebChattingServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("player/partner")]
    public class PlayerPartnerController : ControllerBase
    {
        private readonly IPlayerManager _playerManager;
        private readonly IPlayerPartnerService _playerPartnerService;

        public PlayerPartnerController(IPlayerManager playerManager, IPlayerPartnerService playerPartnerService)
        {
            _playerManager = playerManager;
            _playerPartnerService = playerPartnerService;
        }
        [HttpPost("level-up")]
        public IActionResult LevelUpPartner(LevelUpPartnerDTO partnerDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                Player player = _playerManager.GetPlayer(val);
                bool success = _playerPartnerService.LevelUpPartner(player, partnerDTO.PartnerName, partnerDTO.Level);
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }
        [HttpPost("add-amount")]
        public IActionResult AddPartnerAmount(PartnerAmountDTO partnerDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                Player player = _playerManager.GetPlayer(val);
                bool success = _playerPartnerService.AddPartnerAmount(player, partnerDTO.PartnerName, partnerDTO.Amount);
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }

        [HttpPost("equip")]
        public IActionResult EquipPartner(PartnerEquipDTO partnerDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                Player player = _playerManager.GetPlayer(val);
                bool success = _playerPartnerService.EquipPartner(player, partnerDTO.Idx, partnerDTO.PartnerName);
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }
    }
}
