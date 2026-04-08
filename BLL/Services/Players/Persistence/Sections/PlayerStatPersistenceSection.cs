using BLL.Common.Results;
using BLL.Domain.Players;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerStatPersistenceSection : IPlayerPersistenceSection
    {
        public async Task LoadAsync(int playerId, PlayerState player, IUnitOfWork unitOfWork)
        {
            List<StatVO> stats = await unitOfWork.Stat.GetStats(playerId);
            player.Stats = stats.ToDictionary(item => item.StatType, item => item.Level);
        }

        public async Task<Result> SaveAsync(PlayerState player, IUnitOfWork unitOfWork)
        {
            foreach ((StatType statType, int level) in player.Stats)
            {
                int affected = await unitOfWork.Stat.UpdateStat(player.Id, statType, level);
                if (affected == 0)
                    affected = await unitOfWork.Stat.AddStat(player.Id, statType, level);

                if (affected != 1)
                    return Result.PersistenceFailure($"Failed to persist stat '{statType}'.");
            }

            return Result.Success();
        }
    }
}
