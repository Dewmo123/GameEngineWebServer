using DAL.VOs;

namespace BLL.Caching
{
    public class DefaultSetting
    {
        public static readonly Dictionary<StatType, int> defaultStat = new()
        {
            {StatType.AttackPower,1 },
            {StatType.AttackSpeed,1 },
            {StatType.Health,1 },
            {StatType.CriticalChance,1 },
            {StatType.CriticalDamage,1 },
            {StatType.Hps,1 },
        };
        public static readonly Dictionary<GoodsType, int> defaultGoods = new()
        {
            {GoodsType.Gold, 0 },
            {GoodsType.Crystal, 0 },
            {GoodsType.ReinforceStone, 0 },
            {GoodsType.DungeonKey, 0 },
        };
        public static readonly HashSet<string> skills = new()
        {
            "ReaperSkill"
        };
        public static readonly HashSet<string> partners = new()
        {
            "Axer"
        };
        public static readonly int skillEquipLength = 6;
        public static readonly int partnerEquipLength = 6;
        public static readonly HashSet<int> defaultSkillEquip = new() 
        {
            0,1,2,3,4,5
        };
        public static readonly HashSet<int> defaultPartnerEquip = new()
        {
            0,1,2,3,4,5
        };
    }
}
