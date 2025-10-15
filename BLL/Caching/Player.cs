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
        public string?[] PartnerEquips { get; set; } = new string?[DefaultSetting.partnerEquipLength];
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