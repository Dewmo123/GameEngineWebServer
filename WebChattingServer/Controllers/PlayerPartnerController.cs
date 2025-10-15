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

        [HttpPost("changed")]
        public IActionResult PartnerChanged(PartnerAmountDTO partnerDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                Player player = _playerManager.GetPlayer(val);
                bool success = _playerPartnerService.ChangePartner(player, partnerDTO.PartnerName, partnerDTO.Amount);
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
