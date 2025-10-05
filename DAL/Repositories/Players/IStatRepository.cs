using DAL.VOs;

namespace DAL.Repositories.Players
{
    public interface IStatRepository
    {
        Task<List<StatVO>> GetStatsAsync(int id);
        Task<int> AddStatAsync(int id, StatType statType,int level);
        Task<int> UpdateStatAsync(int id, StatType statType, int level);
    }
}
