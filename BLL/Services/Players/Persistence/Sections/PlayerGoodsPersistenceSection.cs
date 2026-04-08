using BLL.Caching;
using BLL.DTOs;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.Repositories.Players.Goods;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerGoodsPersistenceSection : IPlayerPersistenceSection
    {
        public async Task LoadAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            IGoodsRepository goodsRepository = unitOfWork.GetRepository<IGoodsRepository, GoodsRepository>();
            List<GoodsVO> goods = await goodsRepository.GetAllGoods(playerId);

            player.Goods = goods.ToDictionary(item => item.GoodsType, item => item.Amount);
            foreach ((GoodsType goodsType, int amount) in DefaultSetting.defaultGoods)
            {
                if (player.Goods.ContainsKey(goodsType))
                    continue;

                player.Goods.Add(goodsType, amount);
                await goodsRepository.AddGoodsAsync(playerId, goodsType, amount);
            }
        }

        public async Task<bool> SaveAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            IGoodsRepository goodsRepository = unitOfWork.GetRepository<IGoodsRepository, GoodsRepository>();
            foreach ((GoodsType goodsType, int amount) in player.Goods)
            {
                if (await goodsRepository.UpdateGoods(playerId, goodsType, amount) != 1)
                    return false;
            }

            return true;
        }
    }
}
