using BLL.DTOs;
using DAL.Repositories.Players;
using DAL.VOs;
using static Mysqlx.Notice.Warning.Types;

namespace BLL.Caching
{
    public class Player
    {
        public int Id { get; set; }
        public ReaderWriterLockSlim rwLock;
        public ChapterDTO Chapter { get; set; }
        public Dictionary<StatType, int> Stats { get; set; }
        public Dictionary<GoodsType, int> Goods { get; set; }
        public Dictionary<string, SkillDTO> Skills { get; set; }
        public Player(int id,ChapterDTO chapter, Dictionary<StatType, int> stats, Dictionary<GoodsType, int> goods, Dictionary<string, SkillDTO> skills)
        {
            rwLock = new();
            Id = id;
            Chapter = chapter;
            Stats = stats;
            Goods = goods;
            Skills = skills;
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
        public void LevelUpSkill(string skillName,int level)
        {
            try
            {
                rwLock.EnterWriteLock();
                if (!Skills.ContainsKey(skillName))
                    return;
                SkillDTO dto = Skills[skillName];
                dto.Level += level;
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        public void ChangeSkill(string skillName, int amount,int upgrade)
        {
            try
            {
                rwLock.EnterWriteLock();
                if (!Skills.ContainsKey(skillName))
                    return;
                SkillDTO dto = Skills[skillName];
                dto.Amount = amount;
                dto.Upgrade = upgrade;
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }

        public void AddSkill(SkillDTO skill)
        {
            try
            {
                rwLock.EnterWriteLock();
                if (Skills.ContainsKey(skill.SkillName))
                    return;
                Skills.Add(skill.SkillName, skill);
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        #region Update Region
        public async Task<bool> UpdateStats(IStatRepository statRepo)
        {
            Dictionary<StatType, int> statsCopy;
            try
            {
                rwLock.EnterReadLock();
                statsCopy = new Dictionary<StatType, int>(Stats);
            }
            finally
            {
                rwLock.ExitReadLock();
            }

            bool success = true;
            foreach (var item in statsCopy)
            {
                int affected = await statRepo.UpdateStat(Id, item.Key, item.Value);
                success &= affected == 1;
            }
            return success;
        }
        public async Task<bool> UpdateGoods(IGoodsRepository goodsRepo)
        {
            Dictionary<GoodsType, int> goodsCopy;
            try
            {
                rwLock.EnterReadLock();
                goodsCopy = new Dictionary<GoodsType, int>(Goods);
            }
            finally
            {
                rwLock.ExitReadLock();
            }

            bool success = true;
            foreach (var item in goodsCopy)
            {
                int affected = await goodsRepo.UpdateGoods(Id, item.Key, item.Value);
                success &= affected == 1;
            }
            return success;
        }
        public async Task<bool> UpdateChapter(IChapterRepository chapterRepo)
        {
            ChapterDTO chapterCopy;
            try
            {
                rwLock.EnterReadLock();
                chapterCopy = new ChapterDTO
                {
                    Chapter = Chapter.Chapter,
                    Stage = Chapter.Stage
                };
            }
            finally
            {
                rwLock.ExitReadLock();
            }

            int affected = await chapterRepo.UpdateChapter(Id, chapterCopy.Chapter, chapterCopy.Stage);
            return affected == 1;
        }
        public async Task<bool> UpdateSkill(ISkillRepository skillRepo)
        {
            Dictionary<string, SkillDTO> skillsCopy;
            try
            {
                rwLock.EnterReadLock();
                skillsCopy = new Dictionary<string, SkillDTO>(Skills);
            }
            finally
            {
                rwLock.ExitReadLock();
            }

            bool success = true;
            foreach (var item in skillsCopy)
            {
                int affected = await skillRepo.UpdateSkill(Id, item.Key, item.Value.Level, item.Value.Upgrade, item.Value.Amount);
                if (affected == 0)
                {
                    affected = await skillRepo.AddSkill(Id, item.Key, item.Value.Level, item.Value.Upgrade, item.Value.Amount);
                }
                success &= affected == 1;
            }
            return success;
        }
        #endregion
    }
}
