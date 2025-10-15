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
    [Route("player/goods")]
    public class PlayerGoodsController : ControllerBase
    {
        private readonly IPlayerManager _playerManager;
        private readonly IPlayerGoodsService _playerGoodsService;
        public PlayerGoodsController(IPlayerManager manager, IPlayerGoodsService playerGoodsService)
        {
            _playerManager = manager;
            _playerGoodsService = playerGoodsService;
        }
        [HttpPost("changed")]
        public IActionResult GoodsChanged(GoodsDTO goodsDTO)
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int val))
            {
                Player player = _playerManager.GetPlayer(val);
                bool success = _playerGoodsService.GoodsChanged(player, goodsDTO.GoodsType, goodsDTO.Amount);
                return success ? Ok() : BadRequest();
            }
            return Unauthorized();
        }
    }
}
