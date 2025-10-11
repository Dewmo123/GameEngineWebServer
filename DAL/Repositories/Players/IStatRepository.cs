using DAL.VOs;

namespace DAL.Repositories.Players
{
    public interface IStatRepository
    {
        Task<List<StatVO>> GetStats(int id);
        Task<int> AddStat(int id, StatType statType,int level);
        Task<int> UpdateStat(int id, StatType statType, int level);
    }
}
