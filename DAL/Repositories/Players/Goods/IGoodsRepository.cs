using DAL.VOs;

namespace DAL.Repositories.Players.Goods
{
    public interface IGoodsRepository
    {
        Task<int> UpdateGoods(int id,GoodsType goodsType, int amount);
        Task<int> AddGoodsAsync(int id, GoodsType goodsType, int amount);
        Task<List<GoodsVO>> GetAllGoods(int id);
    }
}
