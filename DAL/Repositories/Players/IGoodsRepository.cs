using DAL.VOs;

namespace DAL.Repositories.Players
{
    public interface IGoodsRepository
    {
        Task<int> UpdateGoods(int id,GoodsType goodsType, int amount);
        Task<int> AddGoods(int id, GoodsType goodsType, int amount);
        Task<List<GoodsVO>> GetAllGoods(int id);
    }
}
