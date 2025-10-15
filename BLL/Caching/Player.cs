using BLL.DTOs;
using DAL.VOs;

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
        public Dictionary<string, PartnerDTO> Partners { get; set; }
        public string?[] SkillEquips { get; set; } = new string?[DefaultSetting.skillEquipLength];
        public Player(int id, PlayerDTO playerInfo)
        {
            rwLock = new();
            Id = id;
            Chapter = playerInfo.Chapter!;
            Stats = playerInfo.Stats!;
            Goods = playerInfo.Goods!;
            Skills = playerInfo.Skills!;
            Partners = playerInfo.Partners!;
            SkillEquips = playerInfo.SkillEquips;
        }
        public bool LevelUpStat(StatType stat, int level)
        {
            if (level <= 0)
                return false;
            try
            {
                rwLock.EnterWriteLock();
                Stats[stat] += level;
                return true;
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        public bool GoodsChanged(GoodsType goods, int amount)
        {
            try
            {
                rwLock.EnterWriteLock();
                if (Goods[goods] + amount < 0)
                    return false;
                Goods[goods] += amount;
                Console.WriteLine(Goods[goods]);
                return true;
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        public void LevelUpSkill(string skillName, int level)
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
        public bool ChangeSkill(string skillName, int amount)
        {
            try
            {
                rwLock.EnterWriteLock();
                if (string.IsNullOrEmpty(skillName) || !DefaultSetting.skills.Contains(skillName))
                    return false;
                Skills[skillName].Amount+=amount;
                return true;
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        public bool EquipSkill(int idx, string? skillName)
        {
            try
            {
                rwLock.EnterWriteLock();
                if (idx < 0 || idx >= SkillEquips.Length)
                    return false;
                if (!string.IsNullOrEmpty(skillName) && !Skills.ContainsKey(skillName))
                    return false;
                SkillEquips[idx] = skillName;
                return true;
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        public bool ChapterChanged(int chapter, int stage)
        {
            try
            {
                rwLock.EnterWriteLock();
                if (chapter <= 0 || stage <= 0)
                    return false;
                Chapter.Chapter = chapter;
                Chapter.Stage = stage;
                Chapter.EnemyCount = 0;
                return true;
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        public void EnemyDead(int count)
        {
            try
            {
                rwLock.EnterWriteLock();
                Chapter.EnemyCount += count;
                Console.WriteLine(Chapter.EnemyCount);
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
                if (Skills.ContainsKey(skill.SkillName) || !DefaultSetting.skills.Contains(skill.SkillName))
                    return;
                Skills.Add(skill.SkillName, skill);
            }
            finally
            {
                rwLock.ExitWriteLock();
            }
        }
        public PlayerDTO GetCopyDTO()
        {
            try
            {
                rwLock.EnterReadLock();
                PlayerDTO playerDTO = new()
                {
                    Chapter = new ChapterDTO { Chapter = Chapter.Chapter, EnemyCount = Chapter.EnemyCount, Stage = Chapter.Stage },
                    Goods = new(Goods),
                    Skills = new(Skills),
                    Stats = new(Stats),
                    Partners = new(Partners),
                    SkillEquips = SkillEquips.ToArray()
                };
                return playerDTO;
            }
            finally
            {
                rwLock.ExitReadLock();
            }
        }
    }
}
