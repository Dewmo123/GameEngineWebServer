using BLL.Caching;
using DAL.VOs;

namespace BLL.Services.Players
{
    public interface IPlayerStatService
    {
        bool LevelUpStat(Player player, StatType stat, int level);
    }
}
