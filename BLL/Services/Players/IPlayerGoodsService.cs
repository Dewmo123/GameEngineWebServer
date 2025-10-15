using BLL.Caching;
using DAL.VOs;

namespace BLL.Services.Players
{
    public interface IPlayerGoodsService
    {
        bool GoodsChanged(Player player, GoodsType goods, int amount);
    }
}
