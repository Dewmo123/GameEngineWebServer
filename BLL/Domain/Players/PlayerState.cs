using BLL.Caching;
using DAL.VOs;

namespace BLL.Domain.Players
{
    public sealed class PlayerChapterState
    {
        public int Chapter { get; set; } = 1;
        public int Stage { get; set; } = 1;
        public int EnemyCount { get; set; }

        public PlayerChapterState Clone()
        {
            return new PlayerChapterState
            {
                Chapter = Chapter,
                Stage = Stage,
                EnemyCount = EnemyCount
            };
        }
    }

    public sealed class PlayerSkillState
    {
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int Upgrade { get; set; }
        public int Amount { get; set; }

        public PlayerSkillState Clone()
        {
            return new PlayerSkillState
            {
                Name = Name,
                Level = Level,
                Upgrade = Upgrade,
                Amount = Amount
            };
        }
    }

    public sealed class PlayerPartnerState
    {
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public int Upgrade { get; set; }
        public int Amount { get; set; }

        public PlayerPartnerState Clone()
        {
            return new PlayerPartnerState
            {
                Name = Name,
                Level = Level,
                Upgrade = Upgrade,
                Amount = Amount
            };
        }
    }

    public sealed class PlayerState
    {
        public int Id { get; init; }
        public PlayerChapterState Chapter { get; set; } = new();
        public Dictionary<StatType, int> Stats { get; set; } = new();
        public Dictionary<GoodsType, int> Goods { get; set; } = new();
        public Dictionary<string, PlayerSkillState> Skills { get; set; } = new(StringComparer.Ordinal);
        public Dictionary<string, PlayerPartnerState> Partners { get; set; } = new(StringComparer.Ordinal);
        public string?[] SkillEquips { get; set; } = new string?[DefaultSetting.SkillEquipLength];
        public string?[] PartnerEquips { get; set; } = new string?[DefaultSetting.PartnerEquipLength];

        public void ApplyDefaults()
        {
            foreach ((StatType statType, int level) in DefaultSetting.DefaultStats)
            {
                if (!Stats.ContainsKey(statType))
                    Stats[statType] = level;
            }

            foreach ((GoodsType goodsType, int amount) in DefaultSetting.DefaultGoods)
            {
                if (!Goods.ContainsKey(goodsType))
                    Goods[goodsType] = amount;
            }

            SkillEquips = NormalizeArray(SkillEquips, DefaultSetting.SkillEquipLength);
            PartnerEquips = NormalizeArray(PartnerEquips, DefaultSetting.PartnerEquipLength);

            if (Chapter.Chapter <= 0)
                Chapter.Chapter = 1;

            if (Chapter.Stage <= 0)
                Chapter.Stage = 1;
        }

        public PlayerState Clone()
        {
            return new PlayerState
            {
                Id = Id,
                Chapter = Chapter.Clone(),
                Stats = new Dictionary<StatType, int>(Stats),
                Goods = new Dictionary<GoodsType, int>(Goods),
                Skills = Skills.ToDictionary(item => item.Key, item => item.Value.Clone(), StringComparer.Ordinal),
                Partners = Partners.ToDictionary(item => item.Key, item => item.Value.Clone(), StringComparer.Ordinal),
                SkillEquips = SkillEquips.ToArray(),
                PartnerEquips = PartnerEquips.ToArray()
            };
        }

        private static string?[] NormalizeArray(string?[] source, int length)
        {
            string?[] normalized = new string?[length];
            for (int i = 0; i < Math.Min(source.Length, normalized.Length); i++)
                normalized[i] = source[i];

            return normalized;
        }
    }
}
