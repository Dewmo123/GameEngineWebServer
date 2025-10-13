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
    [Route("player")]
    public class PlayerController : ControllerBase
    {
        IPlayerService _playerService;
        IPlayerManager _playerManager;
        public PlayerController(IPlayerService playerService, IPlayerManager playerManager)
        {
            _playerService = playerService;
            _playerManager = playerManager;
        }
        [HttpGet("get-player-infos")]
        public async Task<ActionResult<PlayerDTO?>> GetPlayerInfos()
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                PlayerDTO? playerInfo = await _playerService.GetPlayerInfos(val);
                if (playerInfo == null) return default;
                bool success = _playerManager.AddPlayer(val, playerInfo);
                Console.WriteLine($"AddPlayer: {success}");
                return playerInfo;
            }
            return NoContent();
        }
        [HttpDelete("log-out")]
        public async Task LogOut()
        {
            Console.WriteLine("ASD");
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                bool success = await _playerManager.RemovePlayer(val);
                Console.WriteLine($"RemovePlayer: {success}");
            }
        }
        [HttpPost("stat/level-up")]
        public IActionResult StatLevelUp(StatDTO statDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                bool success = _playerManager.GetPlayer(val).LevelUpStat(statDTO.StatType, statDTO.Level); ;
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }
        [HttpPost("goods/changed")]
        public IActionResult GoodsChanged(GoodsDTO goodsDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                bool success = _playerManager.GetPlayer(val).GoodsChanged(goodsDTO.GoodsType, goodsDTO.Amount);
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }
        [HttpPost("skill/changed")]
        public void SkillChanged(SkillDTO skillDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val) && !string.IsNullOrEmpty(skillDTO.SkillName))
            {
                _playerManager.GetPlayer(val).ChangeSkill(skillDTO);
            }
        }
        [HttpPost("stage/changed")]
        public IActionResult StageChanged(ChapterDTO chapter)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                bool success = _playerManager.GetPlayer(val).ChapterChanged(chapter.Chapter, chapter.Stage);
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }
        [HttpPost("stage/enemy-dead")]
        public IActionResult EnemyDead(EnemyDeadDTO enemyDead)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                _playerManager.GetPlayer(val).EnemyDead(enemyDead.EnemyCount);
                return Ok();
            }
            return Unauthorized();
        }
    }
}
