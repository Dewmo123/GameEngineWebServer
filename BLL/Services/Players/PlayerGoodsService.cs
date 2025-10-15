using BLL.Caching;
using DAL.VOs;

namespace BLL.Services.Players
{
    public class PlayerGoodsService : IPlayerGoodsService
    {
        public bool GoodsChanged(Player player, GoodsType goods, int amount)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (player.Goods[goods] + amount < 0)
                    return false;
                player.Goods[goods] += amount;
                Console.WriteLine(player.Goods[goods]);
                return true;
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }
    }
}
