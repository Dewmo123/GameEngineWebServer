using BLL.Common.Results;
using BLL.Domain.Players;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerGoodsPersistenceSection : IPlayerPersistenceSection
    {
        public async Task LoadAsync(int playerId, PlayerState player, IUnitOfWork unitOfWork)
        {
            List<GoodsVO> goods = await unitOfWork.Goods.GetAllGoods(playerId);
            player.Goods = goods.ToDictionary(item => item.GoodsType, item => item.Amount);
        }

        public async Task<Result> SaveAsync(PlayerState player, IUnitOfWork unitOfWork)
        {
            foreach ((GoodsType goodsType, int amount) in player.Goods)
            {
                int affected = await unitOfWork.Goods.UpdateGoods(player.Id, goodsType, amount);
                if (affected == 0)
                    affected = await unitOfWork.Goods.AddGoodsAsync(player.Id, goodsType, amount);

                if (affected != 1)
                    return Result.PersistenceFailure($"Failed to persist goods '{goodsType}'.");
            }

            return Result.Success();
        }
    }
}
