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
    [Route("player/skill")]
    public class PlayerSkillController : ControllerBase
    {
        private readonly IPlayerManager _playerManager;
        private readonly IPlayerSkillService _playerSkillService;
        public PlayerSkillController(IPlayerManager playerManager, IPlayerSkillService playerSkillService)
        {
            _playerManager = playerManager;
            _playerSkillService = playerSkillService;
        }
        [HttpPost("level-up")]
        public IActionResult LevelUpSkill(LevelUpSkillDTO skillDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                Player player = _playerManager.GetPlayer(val);
                bool success = _playerSkillService.LevelUpSkill(player, skillDTO.SkillName, skillDTO.Level);
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }
        [HttpPost("add-amount")]
        public IActionResult AddSkillAmount(SkillAmountDTO skillDTO)//AddAmount로 변경 필요
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                Player player = _playerManager.GetPlayer(val);
                bool success = _playerSkillService.AddSkillAmount(player, skillDTO.SkillName,skillDTO.Amount);
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }
        [HttpPost("equip")]
        public IActionResult equipSkill(SkillEquipDTO skillDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                Player player = _playerManager.GetPlayer(val);
                _playerSkillService.EquipSkill(player, skillDTO.Idx, skillDTO.SkillName);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
