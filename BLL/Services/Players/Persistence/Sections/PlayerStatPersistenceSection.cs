using BLL.Caching;
using BLL.DTOs;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.Repositories.Players.Stat;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerStatPersistenceSection : IPlayerPersistenceSection
    {
        public async Task LoadAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            IStatRepository statRepository = unitOfWork.GetRepository<IStatRepository, StatRepository>();
            List<StatVO> stats = await statRepository.GetStats(playerId);

            player.Stats = stats.ToDictionary(item => item.StatType, item => item.Level);
            foreach ((StatType statType, int level) in DefaultSetting.defaultStat)
            {
                if (player.Stats.ContainsKey(statType))
                    continue;

                player.Stats.Add(statType, level);
                await statRepository.AddStat(playerId, statType, level);
            }
        }

        public async Task<bool> SaveAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            IStatRepository statRepository = unitOfWork.GetRepository<IStatRepository, StatRepository>();
            foreach ((StatType statType, int level) in player.Stats)
            {
                if (await statRepository.UpdateStat(playerId, statType, level) != 1)
                    return false;
            }

            return true;
        }
    }
}
