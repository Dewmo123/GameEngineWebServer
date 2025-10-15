using BLL.Caching;
using BLL.DTOs;
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
        IPlayerManager _playerManager;
        public PlayerGoodsController(IPlayerManager manager)
        {
            _playerManager = manager;
        }
        [HttpPost("changed")]
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
    }
}
