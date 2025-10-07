using DAL.Repositories.Players;
using DAL.VOs;
using static Mysqlx.Notice.Warning.Types;

namespace BLL.Caching
{
    public class Player
    {
        public int Id { get; set; }
        public ReaderWriterLockSlim rwLock;
        public Dictionary<StatType, int> Stats { get; set; }
        public Dictionary<GoodsType, int> Goods { get; set; }
        public Player(int id, Dictionary<StatType, int> stats, Dictionary<GoodsType, int> goods)
        {
            rwLock = new();
            Id = id;
            Stats = stats;
            Goods = goods;
        }
        public void LevelUpStat(StatType stat,int level)
        {
            try
            {
                rwLock.EnterWriteLock();
                Stats[stat] += level;
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        public void AddGoods(GoodsType goods,int amount)
        {
            try
            {
                rwLock.EnterWriteLock();
                Goods[goods] += amount;
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        public async Task<bool> UpdateStats(IStatRepository statRepo)
        {
            try
            {
                rwLock.EnterReadLock();
                bool success = true;
                foreach (var item in Stats)
                {
                    int affected = await statRepo.UpdateStatAsync(Id, item.Key, item.Value);
                    success &= affected == 1;
                }
                return success;
            }
            finally
            {
                rwLock.ExitReadLock();
            }
        }
    }
}
