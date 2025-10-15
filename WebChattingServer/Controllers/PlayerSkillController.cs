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
        IPlayerManager _playerManager;
        public PlayerSkillController(IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
        [HttpPost("changed")]
        public IActionResult SkillChanged(SkillAmountDTO skillDTO)//AddAmount로 변경 필요
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                bool success = _playerManager.GetPlayer(val).ChangeSkill(skillDTO.SkillName,skillDTO.Amount);
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
                _playerManager.GetPlayer(val).EquipSkill(skillDTO.Idx, skillDTO.SkillName);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
