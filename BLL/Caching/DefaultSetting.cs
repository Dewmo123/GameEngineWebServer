using System.Collections.ObjectModel;
using DAL.VOs;

namespace BLL.Caching
{
    public static class DefaultSetting
    {
        public static IReadOnlyDictionary<StatType, int> DefaultStats { get; } = new ReadOnlyDictionary<StatType, int>(new Dictionary<StatType, int>
        {
            { StatType.AttackPower, 1 },
            { StatType.AttackSpeed, 1 },
            { StatType.Health, 1 },
            { StatType.CriticalChance, 1 },
            { StatType.CriticalDamage, 1 },
            { StatType.Hps, 1 },
        });

        public static IReadOnlyDictionary<GoodsType, int> DefaultGoods { get; } = new ReadOnlyDictionary<GoodsType, int>(new Dictionary<GoodsType, int>
        {
            { GoodsType.Gold, 0 },
            { GoodsType.Crystal, 0 },
            { GoodsType.ReinforceStone, 0 },
            { GoodsType.DungeonKey, 0 },
        });

        public static IReadOnlySet<string> SkillNames { get; } = new HashSet<string>(StringComparer.Ordinal)
        {
            "ReaperSkill",
            "BombSkill",
            "FireballSkill",
            "ShunraiSkill",
            "ReaperSmashSkill",
            "HammerSkill",
            "LavaGolemSkill"
        };

        public static IReadOnlySet<string> PartnerNames { get; } = new HashSet<string>(StringComparer.Ordinal)
        {
            "Axer",
            "Riven",
            "Mooni",
            "BigAxer",
            "BigMooni",
            "Reaper",
            "BigReaper"
        };

        public const int SkillEquipLength = 6;
        public const int PartnerEquipLength = 6;
    }
}
