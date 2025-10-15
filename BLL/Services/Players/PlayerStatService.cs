using BLL.Caching;
using DAL.VOs;

namespace BLL.Services.Players
{
    public class PlayerStatService : IPlayerStatService
    {
        public bool LevelUpStat(Player player, StatType stat, int level)
        {
            if (level <= 0)
                return false;
            try
            {
                player.rwLock.EnterWriteLock();
                player.Stats[stat] += level;
                return true;
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }
    }
}
