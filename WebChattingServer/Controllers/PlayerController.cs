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
                _playerManager.AddPlayer(val, playerInfo);
                return playerInfo;
            }
            return NoContent();
        }
        [HttpDelete("log-out")]
        public async Task LogOut()
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                await _playerManager.RemovePlayer(val);
            }
        }
        [HttpPost("stat-up")]
        public void StatLevelUp(StatDTO statDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                _playerManager.GetPlayer(val).LevelUpStat(statDTO.StatType, statDTO.Level);
            }
        }
        [HttpPost("goods-up")]
        public void GoodsLevelUp(GoodsDTO goodsDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                _playerManager.GetPlayer(val).AddGoods(goodsDTO.GoodsType, goodsDTO.Amount);
            }
        }
    }
}
