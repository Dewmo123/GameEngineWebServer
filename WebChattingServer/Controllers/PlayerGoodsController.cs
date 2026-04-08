using BLL.DTOs;
using BLL.Services.Players.Application;
using Microsoft.AspNetCore.Mvc;

namespace WebChattingServer.Controllers
{
    [Route("player/goods")]
    public class PlayerGoodsController : PlayerApiControllerBase
    {
        private readonly IPlayerApplicationService _playerApplicationService;

        public PlayerGoodsController(IPlayerApplicationService playerApplicationService)
        {
            _playerApplicationService = playerApplicationService;
        }

        [HttpPost("changed")]
        public async Task<IActionResult> GoodsChanged([FromBody] GoodsDTO goodsDTO)
        {
            if (!TryGetCurrentPlayerId(out int playerId, out IActionResult? error))
                return error!;

            return ToActionResult(await _playerApplicationService.ChangeGoodsAsync(playerId, goodsDTO));
        }
    }
}
